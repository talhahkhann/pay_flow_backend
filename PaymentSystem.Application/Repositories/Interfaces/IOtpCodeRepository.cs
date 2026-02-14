
using PaymentSystem.Domain.Auth;

public interface IOtpCodeRepository
{
    Task AddAsync(OtpCode otpCode);
    Task<OtpCode?> GetLatestUnusedByUserIdAsync(Guid userId);
    Task<bool> MarkAllActiveOtpsAsUsedForUser(Guid userId);
    Task UpdateAsync(OtpCode otpCode);
}