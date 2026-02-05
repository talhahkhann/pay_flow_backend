using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Infrastructure.DomainEvents;

namespace PaymentSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
