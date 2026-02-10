public interface IOtpService
{
    Task<string> GenerateOtpAsync(Guid userId);
    Task<bool> VerifyOtpAsync(Guid userId, string code);
}
