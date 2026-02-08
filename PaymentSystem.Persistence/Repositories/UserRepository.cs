using Microsoft.EntityFrameworkCore;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Domain.Users;
using PaymentSystem.Persistence.Context;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var identityUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (identityUser == null) return null;

        return new User(identityUser.Id, identityUser.FullName, identityUser.Email!);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var identityUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (identityUser == null) return null;

        return new User(identityUser.Id, identityUser.FullName, identityUser.Email!);
    }
}
