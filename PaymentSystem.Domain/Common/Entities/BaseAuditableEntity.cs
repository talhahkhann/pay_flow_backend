public abstract class BaseAuditableEntity : BaseEntity, IBaseAuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
}
