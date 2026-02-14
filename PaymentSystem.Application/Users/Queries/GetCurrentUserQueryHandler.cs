using MediatR;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.Application.Users.Queries;

public class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IUserRepository _users;

    public GetCurrentUserQueryHandler(
        ICurrentUserService currentUser,
        IUserRepository users)
    {
        _currentUser = currentUser;
        _users = users;
    }

    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (_currentUser.UserId == null)
                return Result<CurrentUserDto>.NotFound("Auth.NotFound", "User not Found in");
            if(_currentUser.IsAuthenticated == false)
                return Result<CurrentUserDto>.Unauthorized("Auth.Unauthorized", "User not Authenticated in");
            var user = await _users.GetByIdAsync(_currentUser.UserId.Value);

            if (user == null)
                return Result<CurrentUserDto>.NotFound("User.NotFound", "User not found");

            var userDto = new CurrentUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty
            };

            return Result<CurrentUserDto>.Success(userDto);
        }
        catch (Exception ex)
        {
            // Optionally log the exception here if you have an ILogger injected
            // _logger?.LogError(ex, "An error occurred while retrieving the current user.");

            return Result<CurrentUserDto>.Failure("User.RetrievalError", "An unexpected error occurred while fetching user data.");
        }
    }
}