// RateLimiterExtensions.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace PaymentSystem.Infrastructure; // Replace with your actual namespace

public static class RateLimiterExtensions
{
    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("OtpPolicy", opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(1);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            });

            // You can add more policies here if needed
        });

        return services;
    }
}