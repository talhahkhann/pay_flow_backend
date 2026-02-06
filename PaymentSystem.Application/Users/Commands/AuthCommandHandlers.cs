using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Users.Commands;

public class AuthCommandHandlers :
    IRequestHandler<RegisterUserCommand, string>,
    IRequestHandler<LoginUserCommand, string>
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

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.RegisterUserAsync(
            request.FullName,
            request.Email,
            request.Password);

        return _jwtTokenGenerator.GenerateToken(userId, request.Email);
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.ValidateUserAsync(
            request.Email,
            request.Password);

        return _jwtTokenGenerator.GenerateToken(userId, request.Email);
    }
}
