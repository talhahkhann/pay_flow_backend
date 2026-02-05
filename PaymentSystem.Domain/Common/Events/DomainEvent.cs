namespace PaymentSystem.Domain.Event;
public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}
