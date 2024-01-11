using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Modules.Payments.Core.Domain;
using NetStore.Modules.Payments.Core.EF;
using NetStore.Modules.Payments.Core.Exceptions;
using NetStore.Modules.Payments.Core.External;
using NetStore.Modules.Payments.Core.Validators;
using NetStore.Modules.Payments.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Core.Services;

internal sealed class PaymentService : IPaymentService
{
    private readonly PaymentValidator _paymentValidator;
    private readonly PaymentsDbContext _dbContext;
    private readonly IPaymentGatewayFacade _paymentGatewayFacade;
    private readonly IModuleRequestDispatcher _moduleRequestDispatcher;
    private readonly IMessageBroker _messageBroker;

    public PaymentService(PaymentValidator paymentValidator, PaymentsDbContext dbContext, IPaymentGatewayFacade paymentGatewayFacade,
         IModuleRequestDispatcher moduleRequestDispatcher, IMessageBroker messageBroker)
    {
        _paymentValidator = paymentValidator;
        _dbContext = dbContext;
        _paymentGatewayFacade = paymentGatewayFacade;
        _moduleRequestDispatcher = moduleRequestDispatcher;
        _messageBroker = messageBroker;
    }
    
    public async Task SetupPayment(SetupPaymentDto paymentSetup)
    {
        _paymentValidator.Validate(paymentSetup);

        var existingPayment = await _dbContext.Payments.SingleOrDefaultAsync(x => x.PaymentId == paymentSetup.PaymentId);

        if (existingPayment is not null)
        {
            throw new PaymentAlreadyExistsException(paymentSetup.PaymentId);
        }

        var customer = await _moduleRequestDispatcher.SendAsync<CustomerInformationDto>(new GetCustomerInformation(paymentSetup.CustomerId));

        if (customer is null)
        {
            throw new CustomerNotFoundException(paymentSetup.CustomerId);
        }
        
        var secret = $"{Guid.NewGuid()}-{Guid.NewGuid()}";

        await _dbContext.Payments.AddAsync(new Payment(
            paymentSetup.PaymentId, 
            paymentSetup.CustomerId,
            paymentSetup.Amount, 
            paymentSetup.DueDate, 
            secret, false));

        var fullName = $"{customer.FirstName} {customer.LastName}";
        var address = $"{customer.City} {customer.Street}";

        await _paymentGatewayFacade.SetUpPayment(paymentSetup, fullName, address, secret);

        await _dbContext.SaveChangesAsync();
    }

    public async Task OnPaymentPayed(PaymentWebhookDto webhookDto)
    {
        var payment = await _dbContext.Payments.SingleOrDefaultAsync(x => x.PaymentId == webhookDto.PaymentId);

        if (payment is null)
        {
            throw new PaymentAlreadyExistsException(webhookDto.PaymentId);
        }

        if (webhookDto.PaymentGatewaySecret != payment.PaymentGatewaySecret)
        {
            throw new PaymentGatewaySecretMismatchException(payment.PaymentId);
        }

        var updatedPayment = payment with { Payed = true };

        _dbContext.Entry(payment).CurrentValues.SetValues(updatedPayment);

        await _messageBroker.PublishAsync(new PaymentCompleted(payment.PaymentId));

        await _dbContext.SaveChangesAsync();
    }
}