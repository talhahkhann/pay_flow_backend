using MediatR;
using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.Application.Users.Commands;

public record RegisterUserCommand(
    string FullName,
    string Email,
    string UserName,
    string Password
) : IRequest<Result<string>>;  // ← Result<string>