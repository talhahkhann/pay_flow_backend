using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.Application.Common.Interfaces;

public interface IUserIdentityService
{
    Task<Result<Guid>> RegisterUserAsync(string fullName, string email, string UserName ,string password);
    Task<Result<Guid>> ValidateUserAsync(string email, string password);
}