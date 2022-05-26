using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.SharedKernel;
using SchoolManagementApp.Domain.Subjects;
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

        public virtual void AssignSubject(Subject subject)
        {
            _subjects.Add(subject);
        }

        public virtual void AssignManySubjects(List<Subject> subjects)
        {
            subjects.ForEach(staffSubject => _subjects.Add(staffSubject));
        }

        public virtual void RemoveSubject(Subject subject)
        {
            _subjects.Remove(subject);
        }

        public virtual void RemoveManySubjects(List<Subject> subjects)
        {
            subjects.ForEach(staffSubject => _subjects.Remove(staffSubject));
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

        private ISet<Subject> _subjects = new HashSet<Subject>();
        public virtual IEnumerable<Subject> Subjects => _subjects;
    }
}
