namespace Shared.Domain.Entities
{
    public class BaseEntity<TId>:AuditEntity
    {
        public virtual TId Id { get; set; }
    }
}
