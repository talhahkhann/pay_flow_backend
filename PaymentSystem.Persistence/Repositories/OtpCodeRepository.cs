// PaymentSystem.Persistence.Repositories.OtpCodeRepository.cs
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Auth;
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

    public async Task<OtpCode?> GetLatestUnusedByUserIdAsync(Guid userId, string code)
    {
        return await _context.OTPs
            .Where(x => x.UserId == userId && x.Code == code && !x.IsUsed)
            .OrderByDescending(x => x.CreatedOn)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(OtpCode otpCode)
    {
        _context.OTPs.Update(otpCode);
        // No SaveChanges here — let unit of work or service handle it
    }
}