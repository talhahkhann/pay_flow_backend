// PaymentSystem.Persistence/DependencyInjection.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Persistence.Auth;
using PaymentSystem.Persistence.Identity;
using PaymentSystem.Persistence.Indentity;
using PaymentSystem.Persistence.Interceptors;
using PaymentSystem.Persistence.Mappings;
using PaymentSystem.Persistence.Payments;
using PaymentSystem.Persistence.Repositories;

namespace PaymentSystem.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register interceptor
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        // Register the service
        services.AddScoped<IUserIdentityService, IdentityService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
        services.AddScoped<IUnitOfWork, AuthUnitOfWork>();
        services.AddAutoMapper(typeof(UserProfile)); // Scans assembly with UserProfile




        // Add DbContext with interceptor
        services.AddDbContext<AuthDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)
            );


            var currentUserService = serviceProvider.GetService<ICurrentUserService>();
            if (currentUserService != null)
            {
                var interceptor = serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                options.AddInterceptors(interceptor);
            }
        });
        services.AddDbContext<PaymentDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(PaymentDbContext).Assembly.FullName)
            );
            

            var currentUserService = serviceProvider.GetService<ICurrentUserService>();
            if (currentUserService != null)
            {
                var interceptor = serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                options.AddInterceptors(interceptor);
            }
        });

        //  Add Identity with Guid key type
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();
        //Register Identity Service
        services.AddScoped<IUserIdentityService, IdentityService>();
        return services;
    }
}