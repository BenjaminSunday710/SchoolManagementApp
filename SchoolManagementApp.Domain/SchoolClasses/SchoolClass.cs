using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Domain.Subjects;
using Shared.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.SchoolClasses
{
    public class SchoolClass:BaseEntity<int>
    {
        protected SchoolClass() { }
        public SchoolClass(string name, School school)
        {
            Name = name;
            school.AddSchoolClass(this);
        }

        public virtual void AssignClassTeacher(AcademicStaff staff)
        {
            staff.SchoolClass = this;
            ClassTeacher = staff;
        }

        public virtual void ChangeClassTeacher(AcademicStaff staff)
        {
            staff.SchoolClass = this;
            ClassTeacher = staff;
        }

        public virtual void HasSubject(Subject subject)
        {
            subject.SchoolClass = this;
            _subjects.Add(subject);
        }

        public virtual void HasManySubjects(IEnumerable<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                _subjects.Add(subject);
                subject.SchoolClass = this;
            }
        }

        public virtual void AddStudent(Student student)
        {
            student.SchoolClass = this;
            _students.Add(student);
        }

        public virtual void AddManyStudents(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                _students.Add(student);
                student.SchoolClass = this;
            }
        } 

        public virtual void RemoveStudent(Student student)
        {
            student.SchoolClass = null;
            _students.Remove(student);
        }

        public virtual void RemoveManyStudents(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                student.SchoolClass = null;
                _students.Remove(student);
            }
        }

        public virtual void HasResult(Result result)
        {
            result.SchoolClass = this;
            _results.Add(result);
        }

        public virtual void HasManyResult(List<Result> results)
        {
            foreach (var result in results)
            {
                result.SchoolClass = this;
                _results.Add(result);
            }
        }

        public virtual void RemoveResult(Result result)
        {
            _results.Remove(result);
        }
        public virtual void RemoveManyResults(List<Result> results)
        {
            results.ForEach(result => _results.Add(result));
        }

        public virtual string Name { get; set; }
        public virtual School School { get; set; }
        public virtual AcademicStaff ClassTeacher { get; protected set; }

        private ISet<Student> _students = new HashSet<Student>();
        public virtual IEnumerable<Student> Students => _students;

        private ISet<Subject> _subjects = new HashSet<Subject>();
        public virtual IEnumerable<Subject> Subjects => _subjects;

        private ISet<Result> _results = new HashSet<Result>();
        public virtual IEnumerable<Result> Results => _results;
    }
}
