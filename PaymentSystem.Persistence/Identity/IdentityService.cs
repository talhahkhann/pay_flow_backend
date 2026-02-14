using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Application.Common.Interfaces;
using PaymentSystem.Application.Common.Models;
using PaymentSystem.Persistence.Indentity;

namespace PaymentSystem.Persistence.Identity;

public class IdentityService : IUserIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<Guid>> RegisterUserAsync(string fullName, string email,string userName,string password )
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                return Result<Guid>.Conflict("User.EmailExists", "Email is already registered");

            var user = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = userName,
                CreatedOn = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => new Error("User.Registration.Failed", e.Description))
                    .ToList();
                
                return Result<Guid>.Failure(errors);
            }

            return Result<Guid>.Success(user.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure("User.Registration.Error", ex.Message);
        }
    }

    public async Task<Result<Guid>> ValidateUserAsync(string email, string password)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<Guid>.Unauthorized("User.InvalidCredentials", "Invalid email or password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
                return Result<Guid>.Unauthorized("User.InvalidCredentials", "Invalid email or password");

            return Result<Guid>.Success(user.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure("User.Login.Error", ex.Message);
        }
    }
}