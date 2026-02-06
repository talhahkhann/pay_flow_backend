using MediatR;

namespace PaymentSystem.Application.Users.Commands
{
    public record RegisterUserCommand(
        string FullName,
        string Email,
        string Password
    ) : IRequest<string>; // returns JWT token
}
