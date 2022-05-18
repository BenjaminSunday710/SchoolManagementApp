using SchoolManagementApp.Domain.Subjects;
using Shared.Domain.Entities;

namespace SchoolManagementApp.Domain.AcademicStaffs
{
    public class AcademicStaffSubject : BaseEntity<int>
    {
        protected AcademicStaffSubject() { }
        public AcademicStaffSubject(Subject subject, AcademicStaff teacher)
        {
            teacher.AssignSubject(this);
            subject.AssignTeacher(this);
        }

        public virtual Subject Subject { get; set; }
        public virtual AcademicStaff Teacher { get; set; }
    }
}
