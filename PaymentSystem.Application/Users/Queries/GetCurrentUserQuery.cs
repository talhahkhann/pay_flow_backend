using MediatR;
using PaymentSystem.Application.Common.Models;

public record GetCurrentUserQuery:IRequest<Result<CurrentUserDto>>;