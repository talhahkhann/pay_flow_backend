using Microsoft.AspNetCore.Identity;

namespace PaymentSystem.Persistence.Indentity
{
    public class ApplicationUser : IdentityUser<Guid>,IBaseAuditableEntity
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get;set;}
        public Guid? LastModifiedBy { get; set;}
    }
}
