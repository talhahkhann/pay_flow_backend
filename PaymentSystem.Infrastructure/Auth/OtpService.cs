
using System.Security.Cryptography;
using PaymentSystem.Domain.Auth; // ← Only depends on Application

public class OtpService : IOtpService
{
    private readonly IOtpCodeRepository _otpRepository;
    private readonly IUnitOfWork _UoW;

    public OtpService(IOtpCodeRepository otpRepository , IUnitOfWork UoW)
    {
        _otpRepository = otpRepository;
        _UoW = UoW;
    }

    public async Task<string> GenerateOtpAsync(Guid userId)
    {
        await _otpRepository.MarkAllActiveOtpsAsUsedForUser(userId);
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var hashedCode = BCrypt.Net.BCrypt.HashPassword(code);
        var otp = new OtpCode(userId, hashedCode, 5); // Domain entity

        await _otpRepository.AddAsync(otp);
        // If repo doesn't call SaveChanges, you may need a UnitOfWork or call it here
        // But better: use explicit UoW or let caller manage transaction

        // For simplicity, assume SaveChanges is called in repo or via injected UoW
        await _UoW.SaveChangesAsync(); //commit transaction
        return code;
    }
    public async Task<bool> InvalidatePreviousOtps(Guid userId)
    {
        return await _otpRepository.MarkAllActiveOtpsAsUsedForUser(userId);
      
    }
    public async Task<bool> VerifyOtpAsync(Guid userId, string hashedcode)
    {
        var otp = await _otpRepository.GetLatestUnusedByUserIdAsync(userId);
        if (otp == null || otp.IsExpired())
            return false;
        if (otp.IsLocked())
        {
            return false;
        }
        bool isValid = BCrypt.Net.BCrypt.Verify(hashedcode, otp.Code);
        if (!isValid)
        {
            otp.RegisterFailedAttempt();
            await _UoW.SaveChangesAsync();
            return false;            
        }


        otp.MarkAsUsed();
        await _otpRepository.UpdateAsync(otp);

        return true;
    }
}