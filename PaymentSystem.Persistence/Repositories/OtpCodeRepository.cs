// PaymentSystem.Persistence.Repositories.OtpCodeRepository.cs
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Auth;
using PaymentSystem.Domain.Users;
using PaymentSystem.Persistence.Auth;

public class OtpCodeRepository : IOtpCodeRepository
{
    private readonly AuthDbContext _context;

    public OtpCodeRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OtpCode otpCode)
    {
        await _context.OTPs.AddAsync(otpCode);
    }

    public async Task<bool> MarkAllActiveOtpsAsUsedForUser(Guid userId)
    {
        var updatedCount = await _context.OTPs
        .Where(x => x.UserId == userId && !x.IsUsed && x.ExpiresAt > DateTime.UtcNow)
        .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsUsed, true));

        return updatedCount > 0; // true if at least one OTP was marked as used
    }
        

    public async Task<OtpCode?> GetLatestUnusedByUserIdAsync(Guid userId)
    {
        return await _context.OTPs
            .Where(x => x.UserId == userId && !x.IsUsed)
            .OrderByDescending(x => x.CreatedOn)
            .FirstOrDefaultAsync();
    }
    

    public async Task UpdateAsync(OtpCode otpCode)
    {
        _context.OTPs.Update(otpCode);
        // No SaveChanges here — let unit of work or service handle it
    }
}