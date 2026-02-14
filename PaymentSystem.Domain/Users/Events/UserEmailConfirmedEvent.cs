

using PaymentSystem.Domain.Event;

namespace PaymentSystem.Domain.Users.Events;

public record UserEmailConfirmedEvent(Guid UserId, string Email) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}