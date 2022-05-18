using System;

namespace Shared.Domain.Entities
{
    public class AuditEntity
    {
        public virtual string CreatedBy { get; set; } = "DbAdmin";
        public virtual DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        public virtual string LastModifiedBy { get; set; }
        public virtual DateTimeOffset LastModified { get; set; }
    }
}
