using System;

namespace Shared.Domain.Entities
{
    public class AuditEntity
    {
        public virtual string CreatedBy { get; set; } = "DbAdmin";
        public virtual DateTime Created { get; set; } = DateTime.UtcNow;
        public virtual string LastModifiedBy { get; set; }
        public virtual DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
