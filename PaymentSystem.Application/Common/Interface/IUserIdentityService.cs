namespace PaymentSystem.Application.Common.Interfaces;
public interface IUserIdentityService
{
    Task<Guid> RegisterUserAsync(string fullName, string email, string password);
    Task<Guid> ValidateUserAsync(string email, string password);
}
