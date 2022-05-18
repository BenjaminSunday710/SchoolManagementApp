using SchoolManagementApp.Domain.AcademicStaffs;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class AcademicStaffSubjectMap:BaseMap<int,AcademicStaffSubject>
    {
        public AcademicStaffSubjectMap()
        {
            Table("AcademicStaffSubjects");
            References(x => x.Subject);
            References(x => x.Teacher);
        }
    }
}
