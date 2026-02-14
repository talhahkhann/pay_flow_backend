using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.Application.Users.Commands;

public class AuthCommandHandlers :
    IRequestHandler<RegisterUserCommand, Result<string>>,  // ← Changed to Result<string>
    IRequestHandler<LoginUserCommand, Result<string>>      // ← Changed to Result<string>
{
    private readonly IUserIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IOtpService _otpService;

    public AuthCommandHandlers(
        IUserIdentityService identityService,
        IJwtTokenGenerator jwtTokenGenerator, IEmailSender emailSender , IOtpService otpService)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
        _otpService = otpService;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userIdResult = await _identityService.RegisterUserAsync(
                request.FullName,
                request.Email,
                request.UserName,
                request.Password
                );

            if (userIdResult.IsFailure)
                return Result<string>.Failure(userIdResult.Error);
            // Generate OTP
            var otp = await _otpService.GenerateOtpAsync(userIdResult.Value);
            //Send Email
            await _emailSender.SendOtpEmailAsync(request.Email, otp);
            // var token = _jwtTokenGenerator.GenerateToken(userIdResult.Value, request.Email);
            
            return Result<string>.Success("Verification OTP Sent to Email");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure("User.Registration.Failed", ex.Message);
        }
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userIdResult = await _identityService.ValidateUserAsync(
                request.Email,
                request.Password);

            if (userIdResult.IsFailure)
                return Result<string>.Failure(userIdResult.Error);
            var otp = await _otpService.GenerateOtpAsync(userIdResult.Value);
            await _emailSender.SendOtpEmailAsync(request.Email, otp);
            
            return Result<string>.Success("Otp sent to Email");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure("User.Login.Failed", ex.Message);
        }
    }
}