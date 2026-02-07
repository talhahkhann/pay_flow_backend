using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Common.Models;
using PaymentSystem.Application.Common.Security;

namespace PaymentSystem.Application.Common.Behaviours;

public class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUser;

    public AuthorizationBehavior(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType()
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .Cast<AuthorizeAttribute>()
            .ToList();

        if (!authorizeAttributes.Any())
            return await next();

        if (!_currentUser.IsAuthenticated)
            return (TResponse)(object)Result.Failure(
                Error.Unauthorized("Auth.Unauthorized", "User is not authenticated"));

        foreach (var attribute in authorizeAttributes)
        {
            if (attribute.Roles.Length > 0 &&
                !_currentUser.Roles.Any(r => attribute.Roles.Contains(r)))
            {
                return (TResponse)(object)Result.Failure(
                    Error.Unauthorized("Auth.Forbidden", "User does not have required role"));
            }
        }

        return await next();
    }
}
