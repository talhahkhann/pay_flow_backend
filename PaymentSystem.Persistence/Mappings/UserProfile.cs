// PaymentSystem.Persistence/Mappings/UserProfile.cs
using AutoMapper;
using PaymentSystem.Domain.Users;
using PaymentSystem.Persistence.Identity;
using PaymentSystem.Persistence.Indentity;

namespace PaymentSystem.Persistence.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, User>()
            .ConstructUsing(src => new User(
                src.Id,
                src.FullName,
                src.Email ?? string.Empty,
                src.EmailConfirmed
            ));

        // If you need reverse mapping (e.g., for updates)
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()));
    }
}