
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
        var code = new Random().Next(100000, 999999).ToString();
        var otp = new OtpCode(userId, code, 5); // Domain entity

        await _otpRepository.AddAsync(otp);
        // If repo doesn't call SaveChanges, you may need a UnitOfWork or call it here
        // But better: use explicit UoW or let caller manage transaction

        // For simplicity, assume SaveChanges is called in repo or via injected UoW
        await _UoW.SaveChangesAsync(); //commit transaction
        return code;
    }

    public async Task<bool> VerifyOtpAsync(Guid userId, string code)
    {
        var otp = await _otpRepository.GetLatestUnusedByUserIdAsync(userId, code);

        if (otp == null || otp.IsExpired())
            return false;

        otp.MarkAsUsed();
        await _otpRepository.UpdateAsync(otp);

        return true;
    }
}