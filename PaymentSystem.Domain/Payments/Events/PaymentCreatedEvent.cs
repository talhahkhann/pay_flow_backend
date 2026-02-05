public class PaymentCreatedEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public PaymentCreatedEvent(Guid paymentId, Guid userId, decimal amount)
    {
        PaymentId = paymentId;
        UserId = userId;
        Amount = amount;
    }
}
