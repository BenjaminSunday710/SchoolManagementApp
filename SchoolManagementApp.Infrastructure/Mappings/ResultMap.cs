using SchoolManagementApp.Domain.Results;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class ResultMap:BaseMap<Guid, Result>
    {
        public ResultMap()
        {
            Table("Results");
            Map(x => x.Session);
            Map(x => x.Term).CustomType<GenericEnumMapper<Term>>().Not.Nullable();
            Map(x => x.ContinuousAssessment);
            Map(x => x.Examination);
            Map(x => x.Total);
            Map(x => x.Grade).CustomType<GenericEnumMapper<Grade>>().Not.Nullable();
            Map(x => x.Remark).CustomType<GenericEnumMapper<Remark>>().Not.Nullable();
            References(x => x.SchoolClass);
            References(x => x.Student);
            References(x => x.Subject);
        }
    }
}
