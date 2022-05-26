using SchoolManagementApp.Domain.Subjects;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class SubjectMap:BaseMap<Guid, Subject>
    {
        public SubjectMap()
        {
            Table("Subjects");
            Map(x => x.Name);
            References(x => x.SchoolClass);
            HasManyToMany(x => x.Students).Inverse().Cascade.All().Table("StudentSubjects");
            HasManyToMany(x => x.Teachers).Inverse().Cascade.All().Table("AcademicStaffSubjects");
            HasMany(x => x.Results).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
