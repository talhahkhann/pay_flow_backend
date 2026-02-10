public class EmailSender : IEmailSender
{
    public Task SendOtpEmailAsync(string Email , string code)
    {
        return Task.CompletedTask;
    }
}