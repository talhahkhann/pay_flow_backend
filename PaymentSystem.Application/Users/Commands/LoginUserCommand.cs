using MediatR;

namespace PaymentSystem.Application.Users.Commands
{
    public record LoginUserCommand(
        string Email,
        string Password
    ) : IRequest<string>; // returns JWT token
}
