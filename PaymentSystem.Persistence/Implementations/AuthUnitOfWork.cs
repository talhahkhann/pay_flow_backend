using PaymentSystem.Persistence.Auth;

public class AuthUnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _context;
    public AuthUnitOfWork(AuthDbContext context) => _context = context;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _context.SaveChangesAsync(ct);
}