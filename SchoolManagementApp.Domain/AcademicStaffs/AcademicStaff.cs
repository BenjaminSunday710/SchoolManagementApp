using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.SharedKernel;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.AcademicStaffs
{
    public class AcademicStaff:Staff
    {
        protected AcademicStaff() { }
        public AcademicStaff(Person person, School school, string designation=null)
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
        }

        public virtual void AssignSubject(AcademicStaffSubject staffSubject)
        {
            _subjects.Add(staffSubject);
            staffSubject.Teacher = this;
        }

        public virtual void AssignManySubjects(List<AcademicStaffSubject> staffSubjects)
        {
            staffSubjects.ForEach(staffSubject => staffSubject.Teacher = this);
            staffSubjects.ForEach(staffSubject => _subjects.Add(staffSubject));
        }

        public virtual void RemoveSubject(AcademicStaffSubject staffSubject)
        {
            _subjects.Remove(staffSubject);
        }

        public virtual void RemoveManySubjects(List<AcademicStaffSubject> staffSubjects)
        {
            staffSubjects.ForEach(staffSubject => _subjects.Remove(staffSubject));
        }

        public virtual void AssignSchool(School school)
        {
            school.EmployAcademicStaff(this);
        }

        public virtual void AssignFormClass(SchoolClass schoolClass)
        {
            schoolClass.AssignClassTeacher(this);
        }

        public virtual SchoolClass SchoolClass { get; protected internal set; }

        private ISet<AcademicStaffSubject> _subjects = new HashSet<AcademicStaffSubject>();
        public virtual IEnumerable<AcademicStaffSubject> Subjects => _subjects;
    }
}
