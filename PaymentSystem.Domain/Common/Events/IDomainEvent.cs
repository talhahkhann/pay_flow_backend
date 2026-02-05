namespace PaymentSystem.Domain.Event;
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
