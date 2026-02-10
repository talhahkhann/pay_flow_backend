using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using sib_api_v3_sdk.Client;
using Microsoft.Extensions.Configuration;

namespace PaymentSystem.Infrastructure.Email;

public class BrevoEmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public BrevoEmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async System.Threading.Tasks.Task SendOtpEmailAsync(string email, string code)
    {
        var apiKey = _config["Brevo:ApiKey"];

        Configuration.Default.AddApiKey("api-key", apiKey);

        var apiInstance = new TransactionalEmailsApi();

        var emailRequest = new SendSmtpEmail(
            sender: new SendSmtpEmailSender(
                _config["Brevo:FromEmail"],
                _config["Brevo:FromName"]
            ),
            to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email) },
            subject: "Your OTP Code",
            htmlContent: $"<h2>Your OTP Code: {code}</h2>",
            textContent: $"Your OTP Code: {code}"
        );

        await apiInstance.SendTransacEmailAsync(emailRequest);
    }

    
}
