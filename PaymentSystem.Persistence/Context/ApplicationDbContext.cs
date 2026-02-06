// PaymentSystem.Persistence/Context/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Persistence.Indentity;

namespace PaymentSystem.Persistence.Context;

// Change from DbContext to IdentityDbContext
public class ApplicationDbContext : IdentityDbContext<
ApplicationUser,      // TUser
    IdentityRole<Guid>,   // TRole
    Guid,                 // TKey (primary key type)
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>
>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Your custom entities
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //  IMPORTANT: Call base first to configure Identity tables
        base.OnModelCreating(builder);
        
        // Apply your custom configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}