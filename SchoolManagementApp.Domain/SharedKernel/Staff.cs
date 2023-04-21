using SchoolManagementApp.Domain.Schools;

namespace SchoolManagementApp.Domain.SharedKernel
{
    public class Staff:Person
    {
        public virtual string Designation { get; set; }
        public virtual School School { get; set; }
        public virtual string EmploymentId { get; set; }
    }
}
