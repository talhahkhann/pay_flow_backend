public interface IBaseAuditableEntity
{
    DateTime CreatedOn { get; set; }
    string CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    string? LastModifiedBy { get; set; }
}
