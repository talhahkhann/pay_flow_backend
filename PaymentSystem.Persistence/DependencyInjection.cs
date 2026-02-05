using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Persistence.Context;
using PaymentSystem.Persistence.Interceptors;

namespace PaymentSystem.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register DbContext with interceptor
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var currentUserProvider = serviceProvider.GetRequiredService<ICurrentUserService>();
                var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserProvider.UserId);

                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                       .AddInterceptors(interceptor);
            });

            // Register repositories, if you have generic or specific repositories
            // services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}
