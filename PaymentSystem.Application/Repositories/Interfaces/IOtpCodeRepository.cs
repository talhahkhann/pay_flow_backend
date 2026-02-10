
using PaymentSystem.Domain.Auth;

public interface IOtpCodeRepository
{
    Task AddAsync(OtpCode otpCode);
    Task<OtpCode?> GetLatestUnusedByUserIdAsync(Guid userId, string code);
    Task UpdateAsync(OtpCode otpCode);
}