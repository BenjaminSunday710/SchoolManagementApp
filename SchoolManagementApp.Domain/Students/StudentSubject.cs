using SchoolManagementApp.Domain.Subjects;
using Shared.Domain.Entities;

namespace SchoolManagementApp.Domain.Students
{
    public class StudentSubject:BaseEntity<int>
    {
        protected StudentSubject() { }
        public StudentSubject( Student student, Subject subject)
        {
            student.OffersSubject(this);
            subject.RegistersStudent(this);
        }

        public virtual Subject Subject { get; set; }
        public virtual Student Student { get; set; }
    }
}
