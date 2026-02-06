using MediatR;
using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.Application.Users.Commands;

public record LoginUserCommand(
    string Email,
    string Password
) : IRequest<Result<string>>;  // ← Result<string>