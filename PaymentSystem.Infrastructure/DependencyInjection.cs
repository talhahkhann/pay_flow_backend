using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Infrastructure.DomainEvents;
using PaymentSystem.Infrastructure.Email;
using PaymentSystem.Infrastructure.Services;

namespace PaymentSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Domain Events
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // JWT Configuration and Service
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IEmailSender, BrevoEmailSender>();

        // Add other infrastructure services here
        services.AddCustomRateLimiting();
        // services.AddScoped<IEmailService, EmailService>();
        // services.AddScoped<IFileStorage, AzureBlobStorage>();

        return services;
    }
}