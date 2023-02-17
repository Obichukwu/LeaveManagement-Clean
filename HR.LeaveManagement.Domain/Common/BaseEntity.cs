namespace HR.LeaveManagement.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public abstract class AuditableBaseEntity : BaseEntity
{
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

}