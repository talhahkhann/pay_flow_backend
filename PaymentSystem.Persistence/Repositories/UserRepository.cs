// PaymentSystem.Persistence/Repositories/UserRepository.cs
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Domain.Users;
using PaymentSystem.Persistence.Indentity;

namespace PaymentSystem.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        return appUser == null ? null : _mapper.Map<User>(appUser);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());
        return appUser == null ? null : _mapper.Map<User>(appUser);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existing = await _userManager.FindByIdAsync(user.Id.ToString());
        if (existing == null) return false;

        _mapper.Map(user, existing); // Updates existing ApplicationUser
        var result = await _userManager.UpdateAsync(existing);
        return result.Succeeded ? result.Succeeded : false;
    }
}