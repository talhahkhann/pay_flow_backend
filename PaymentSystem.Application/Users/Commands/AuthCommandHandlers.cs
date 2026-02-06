using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Common.Models;
using PaymentSystem.Application.Users.Commands;

namespace PaymentSystem.Application.Users.Commands;

public class AuthCommandHandlers :
    IRequestHandler<RegisterUserCommand, Result<string>>,  // ← Changed to Result<string>
    IRequestHandler<LoginUserCommand, Result<string>>      // ← Changed to Result<string>
{
    private readonly IUserIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthCommandHandlers(
        IUserIdentityService identityService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userIdResult = await _identityService.RegisterUserAsync(
                request.FullName,
                request.Email,
                request.Password);

            if (userIdResult.IsFailure)
                return Result<string>.Failure(userIdResult.Error);

            var token = _jwtTokenGenerator.GenerateToken(userIdResult.Value, request.Email);
            
            return Result<string>.Success(token);
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

            var token = _jwtTokenGenerator.GenerateToken(userIdResult.Value, request.Email);
            
            return Result<string>.Success(token);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure("User.Login.Failed", ex.Message);
        }
    }
}