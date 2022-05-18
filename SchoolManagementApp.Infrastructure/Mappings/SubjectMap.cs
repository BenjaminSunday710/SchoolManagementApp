using SchoolManagementApp.Domain.Subjects;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class SubjectMap:BaseMap<int,Subject>
    {
        public SubjectMap()
        {
            Table("Subjects");
            Map(x => x.Name);
            References(x => x.SchoolClass);
            HasMany(x => x.Students).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Teachers).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Results).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
