using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Students;
using Shared.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.Subjects
{
    public class Subject:BaseEntity<int>
    {
        protected Subject() { }
        public Subject(string name, SchoolClass schoolClass)
        {
            Name = name;
            schoolClass.HasSubject(this);
        }

        public virtual void AssignClass(SchoolClass schoolClass)
        {
            schoolClass.HasSubject(this);
        }

        public virtual void AssignTeacher(AcademicStaffSubject staffSubject)
        {
            _teachers.Add(staffSubject);
            staffSubject.Subject = this;
        }

        public virtual void AssignManyTeachers(List<AcademicStaffSubject> staffSubjects)
        {
            foreach (var staff in staffSubjects)
            {
                _teachers.Add(staff);
                staff.Subject = this;
            }
        }

        public virtual void RemoveTeacher(AcademicStaffSubject staffSubject)
        {
            _teachers.Remove(staffSubject);
        }

        public virtual void RegistersStudent(StudentSubject studentSubject)
        {
            _students.Add(studentSubject);
            studentSubject.Subject = this;
        }

        public virtual void OffersManySubjects(List<StudentSubject> studentSubjects)
        {
            foreach (var student in studentSubjects)
            {
                _students.Add(student);
                student.Subject = this;
            }
        }

        public virtual void RemoveSubject(StudentSubject studentSubject)
        {
            _students.Remove(studentSubject);
        }

        public virtual void RemoveManySubjects(List<StudentSubject> studentSubjects)
        {
            studentSubjects.ForEach(subject => _students.Remove(subject));
        }

        public virtual void RemoveManyTeachers(List<AcademicStaffSubject> staffSubjects)
        {
            foreach (var subject in staffSubjects)
            {
                _teachers.Add(subject);
                subject.Subject = this;
            }
        }

        public virtual void HasResult(Result result)
        {
            result.Subject = this;
            _results.Add(result);
        }

        public virtual void HasManyResult(List<Result> results)
        {
            foreach (var result in results)
            {
                _results.Add(result);
                result.Subject = this;
            }
        }

        public virtual void RemoveResult(Result result)
        {
            _results.Remove(result);
        }
        public virtual void RemoveManyResults(List<Result> results)
        {
            foreach (var result in results)
            {
                _results.Remove(result);
                result.Student = null;
            }
        }

        public virtual string Name { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }

        private ISet<AcademicStaffSubject> _teachers = new HashSet<AcademicStaffSubject>();
        public virtual IEnumerable<AcademicStaffSubject> Teachers => _teachers;

        private ISet<StudentSubject> _students = new HashSet<StudentSubject>();
        public virtual IEnumerable<StudentSubject> Students => _students;

        private ISet<Result> _results = new HashSet<Result>();
        public virtual IEnumerable<Result> Results => _results;
    }
}
