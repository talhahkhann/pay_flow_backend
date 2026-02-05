using PaymentSystem.Domain.Event;

public class PaymentCompletedEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public decimal Amount { get; }

    public PaymentCompletedEvent(Guid paymentId, decimal amount)
    {
        PaymentId = paymentId;
        Amount = amount;
    }
}
