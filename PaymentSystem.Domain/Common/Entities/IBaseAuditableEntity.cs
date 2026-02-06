public interface IBaseAuditableEntity
{
    DateTime CreatedOn { get; set; }
    Guid CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    Guid? LastModifiedBy { get; set; }
}
