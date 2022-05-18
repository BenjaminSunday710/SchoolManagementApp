using SchoolManagementApp.Domain.Results;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class ResultMap:BaseMap<int,Result>
    {
        public ResultMap()
        {
            Table("Results");
            Map(x => x.Session);
            Map(x => x.Term);
            Map(x => x.ContinuousAssessment);
            Map(x => x.Examination);
            Map(x => x.Total);
            Map(x => x.Grade);
            Map(x => x.Remark);
            References(x => x.SchoolClass);
            References(x => x.Student);
            References(x => x.Subject);
        }
    }
}
