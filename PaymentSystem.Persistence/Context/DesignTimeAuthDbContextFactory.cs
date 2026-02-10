using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PaymentSystem.Persistence.Auth;

namespace PaymentSystem.Persistence;

public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection"); // or a dedicated "AuthConnection"
        optionsBuilder.UseNpgsql(connectionString, npgsql => npgsql.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName));

        return new AuthDbContext(optionsBuilder.Options);
    }
}