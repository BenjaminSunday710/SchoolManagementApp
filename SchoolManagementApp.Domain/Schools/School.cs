using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.SharedKernel;
using SchoolManagementApp.Domain.Students;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.Schools
{
    public class School:BaseEntity<Guid>
    {
        protected School() { }
        public School(string name, string website=null)
        {
            Name = name;
            Website = website;
        }

        public virtual void ProvideLocation(string city, string street, int house_Number)
        {
            Location = new Address(city, street, house_Number);
        }

        public virtual void RegisterStudent(Student student)
        {
            student.School = this;
            _students.Add(student);
        }

        public virtual void RegisterManyStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                _students.Add(student);
                student.School = this;
            }
        }

        public virtual void RemoveStudent(Student student)
        {
            student.School = null;
            _students.Remove(student);
        }

        public virtual void RemoveManyStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                _students.Remove(student);
                student.School = null;
            }
        }

        public virtual void EmployAcademicStaff(AcademicStaff staff)
        {
            staff.School = this;
            _academicStaffs.Add(staff);
        } 

        public virtual void EmployManyAcademicStaff(List<AcademicStaff> staffs)
        {
            foreach (var staff in staffs)
            {
                _academicStaffs.Remove(staff);
                staff.School = this;
            }
        } 

        public virtual void RemoveAcademicStaff(AcademicStaff staff)
        {
            staff.School = null;
            _academicStaffs.Remove(staff);
        } 

        public virtual void RemoveManyAcademicStaffs(List<AcademicStaff> staffs)
        {
            foreach (var staff in staffs)
            {
                _academicStaffs.Remove(staff);
                staff.School = null;
            }
        }
        
        public virtual void EmployNonAcademicStaff(NonAcademicStaff staff)
        {
            staff.School = this;
            _nonAcademicStaffs.Add(staff);
        } 

        public virtual void EmployManyNonAcademicStaffs(List<NonAcademicStaff> staffs)
        {
            foreach (var staff in staffs)
            {
                _nonAcademicStaffs.Add(staff);
                staff.School = this;
            }
        } 

        public virtual void RemoveManyNonAcademicStaffs(List<NonAcademicStaff> staffs)
        {
            foreach (var staff in staffs)
            {
                staff.School = null;
                _nonAcademicStaffs.Remove(staff);
            }
        }

        public virtual void RemoveNonAcademicStaff(NonAcademicStaff staff)
        {
            staff.School = this;
            _nonAcademicStaffs.Remove(staff);
        }

        public virtual void AddSchoolClass(SchoolClass schoolClass)
        {
            schoolClass.School = this;
            _schoolClasses.Add(schoolClass);
        }
        public virtual void AddManySchoolClasses(List<SchoolClass> schoolClasses)
        {
            foreach (var schoolClass in schoolClasses)
            {
                schoolClass.School = this;
                _schoolClasses.Add(schoolClass);
            }
        }

        public virtual string Name { get; set; }
        public virtual string Website { get; set; }
        public virtual string StaffIdFormat { get; set; }
        public virtual string StudentIdFormat { get; set; }
        public virtual int LastStaffIdIndex { get; set; }
        public virtual int LastStudentIdIndex { get; set; }
        public virtual Address Location { get; set; }


        private readonly ISet<Student> _students = new HashSet<Student>();
        public virtual IEnumerable<Student> Students => _students;


        private readonly ISet<SchoolClass> _schoolClasses = new HashSet<SchoolClass>();
        public virtual IEnumerable<SchoolClass> SchoolClasses => _schoolClasses;


        private readonly ISet<AcademicStaff> _academicStaffs = new HashSet<AcademicStaff>();
        public virtual IEnumerable<AcademicStaff> AcademicStaffs => _academicStaffs;


        private readonly ISet<NonAcademicStaff> _nonAcademicStaffs = new HashSet<NonAcademicStaff>();
        public virtual IEnumerable<NonAcademicStaff> NonAcademicStaffs => _nonAcademicStaffs;

    }
}
