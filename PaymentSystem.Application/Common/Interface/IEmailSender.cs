public interface IEmailSender
{
    Task SendOtpEmailAsync(string Email, string code);
}