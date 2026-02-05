public interface IDomainEventDispatcher
{
    Task DispatchAsync(BaseEntity entity);
}
