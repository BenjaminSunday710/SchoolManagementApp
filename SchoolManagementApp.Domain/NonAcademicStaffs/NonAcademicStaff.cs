using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.SharedKernel;

namespace SchoolManagementApp.Domain.NonAcademicStaffs
{
    public class NonAcademicStaff:Staff
    {
        protected NonAcademicStaff() { }
        public NonAcademicStaff(Person person, School school, Unit unit, string designation=null)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            LG_Of_Origin = person.LG_Of_Origin;
            StateOfOrigin = person.StateOfOrigin;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;
            Address = person.Address;
            Designation = designation;
            PhoneNumber = person.PhoneNumber;
            Unit = unit;
            school.EmployNonAcademicStaff(this);
        }

        public virtual Unit Unit { get; set; }
    }
}
