using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly string _currentUser;

    public AuditableEntitySaveChangesInterceptor(string currentUser)
    {
        _currentUser = currentUser;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context!;
        var entries = context.ChangeTracker
            .Entries<IBaseAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.CreatedBy = _currentUser;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = _currentUser;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
