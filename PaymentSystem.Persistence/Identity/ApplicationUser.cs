using Microsoft.AspNetCore.Identity;

namespace PaymentSystem.Persistence.Indentity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }
}
