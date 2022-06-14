using System;

namespace Shared.Domain.Entities
{
    public class BaseEntity<TId>:AuditEntity,IEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
