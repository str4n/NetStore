namespace NetStore.Shared.Types.DTO;

public record PaymentDto(Guid Id, double Amount, string PaymentGatewaySecret);