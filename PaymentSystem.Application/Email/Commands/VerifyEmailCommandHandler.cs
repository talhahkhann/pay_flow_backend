using MediatR;
using PaymentSystem.Application.Common.Models;
using PaymentSystem.Application.Common.Interfaces;

namespace PaymentSystem.Application.Auth.Commands;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<string>>
{
    private readonly IOtpService _otpService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userrepository;

    public VerifyEmailCommandHandler(

        IOtpService otpService,
        IUserRepository userrepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _otpService = otpService;
        _userrepository = userrepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Find user by email
        var user = await _userrepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<string>.Failure("User.NotFound", "User not found.");
        }

        // Step 2: Verify OTP
        var isOtpValid = await _otpService.VerifyOtpAsync(user.Id, request.Otp);
        if (!isOtpValid)
        {
            return Result<string>.Failure("OTP.Invalid", "Invalid or expired OTP.");
        }

        // Step 3: Confirm email
        user.ConfirmEmail();

        var success = await _userrepository.UpdateAsync(user);
        if (!success)
        {
            // Log the actual Identity errors if needed (e.g., via ILogger)
            return Result<string>.Failure("User.UpdateFailed", "Failed to confirm email.");
        }

        // Step 4: Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);

        return Result<string>.Success(token);
    }
}