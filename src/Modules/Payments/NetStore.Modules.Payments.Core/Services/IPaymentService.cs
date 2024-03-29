﻿using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Core.Services;

public interface IPaymentService
{
    Task SetupPayment(SetupPaymentDto paymentSetup);
    Task OnPaymentPayed(PaymentWebhookDto webhookDto);
}