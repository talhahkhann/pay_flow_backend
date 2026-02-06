public abstract class BaseAuditableEntity : BaseEntity, IBaseAuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; } = default!;
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
