using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Students;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.Subjects
{
    public class Subject:BaseEntity<Guid>
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

        private ISet<AcademicStaff> _teachers = new HashSet<AcademicStaff>();
        public virtual IEnumerable<AcademicStaff> Teachers => _teachers;

        private ISet<Student> _students = new HashSet<Student>();
        public virtual IEnumerable<Student> Students => _students;

        private ISet<Result> _results = new HashSet<Result>();
        public virtual IEnumerable<Result> Results => _results;
    }
}
