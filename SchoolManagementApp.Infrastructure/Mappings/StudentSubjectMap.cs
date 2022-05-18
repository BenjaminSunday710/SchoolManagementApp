using SchoolManagementApp.Domain.Students;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class StudentSubjectMap:BaseMap<int,StudentSubject>
    {
        public StudentSubjectMap()
        {
            Table("StudentSubjects");
            References(x => x.Subject);
            References(x => x.Student);
        }
    }
}
