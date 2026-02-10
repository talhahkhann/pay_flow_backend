using PaymentSystem.Domain.Users;
namespace PaymentSystem.Application.Common.Interfaces;
public interface  IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> UpdateAsync(User user);
    
}