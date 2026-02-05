using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Domain.Event;

namespace PaymentSystem.Infrastructure.DomainEvents;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync(DomainEvent domainEvent)
    {
        await _mediator.Publish(domainEvent);
    }

    public Task DispatchAsync(BaseEntity entity)
    {
        throw new NotImplementedException();
    }
}
