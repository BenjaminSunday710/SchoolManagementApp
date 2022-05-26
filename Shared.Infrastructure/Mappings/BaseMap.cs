using FluentNHibernate.Mapping;
using Shared.Domain.Entities;

namespace Shared.Infrastructure.Mappings
{
    public class BaseMap<TIdType, TEntity> : ClassMap<TEntity>
        where TEntity : BaseEntity<TIdType>
    {
        public BaseMap()
        {
            Id(x => x.Id).GeneratedBy.Guid();
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.LastModified);
            Map(x => x.LastModifiedBy);
        }
    }
}
