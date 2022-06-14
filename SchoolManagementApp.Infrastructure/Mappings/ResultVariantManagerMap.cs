using SchoolManagementApp.Domain.Results;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class ResultVariantManagerMap:BaseMap<Guid,ResultVariantManager>
    {
        public ResultVariantManagerMap()
        {
            Table("ResultVariantManagers");
            Map(x => x.Session);
            Map(x => x.Term).CustomType<GenericEnumMapper<Term>>().Not.Nullable();
            HasMany(x => x.Results);
        }
    }
}
