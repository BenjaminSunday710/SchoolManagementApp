using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.SharedKernel;
using SchoolManagementApp.Domain.Subjects;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.Students
{
    public class Student : Person
    {
        protected Student() { }
        public Student(Person person,School school,SchoolClass schoolClass)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            LG_Of_Origin = person.LG_Of_Origin;
            StateOfOrigin = person.StateOfOrigin;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;
            Address = person.Address;
            PhoneNumber = person.PhoneNumber;
            school.RegisterStudent(this);
            schoolClass.AddStudent(this);
        }

        public virtual void BelongsToClass(SchoolClass schoolClass)
        {
            schoolClass.AddStudent(this);
        }

        public virtual void UpdateClass(SchoolClass schoolClass)
        {
            this.SchoolClass = null;
            schoolClass.AddStudent(this);
        }

        public virtual void BelongsToSchool(School school)
        {
            school.RegisterStudent(this);
        }

        public virtual void ChangeSchool(School school)
        {
            this.School = null;
            school.RegisterStudent(this);
        }

        public virtual void OffersSubject(Subject subject)
        {
            _subjects.Add(subject);
        }

        public virtual void OffersManySubjects(List<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                _subjects.Add(subject);
            }
        }

        public virtual void RemoveSubject(Subject subject)
        {
            _subjects.Remove(subject);
        }

        public virtual void RemoveManySubjects(List<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                _subjects.Remove(subject);
            }
        }

        public virtual void HasResult(Result result)
        {
            result.Student = this;
            _results.Add(result);
        }

        public virtual void HasManyResult(List<Result> results)
        {
            foreach (var result in results)
            {
                _results.Add(result);
                result.Student = this;
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
                result.Subject = null;
            }
        }

        public virtual SchoolClass SchoolClass { get; set; }
        public virtual School School { get; set; }

        private ISet<Subject> _subjects = new HashSet<Subject>();
        public virtual IEnumerable<Subject> Subjects => _subjects;
        
        private ISet<Result> _results = new HashSet<Result>();
        public virtual IEnumerable<Result> Results => _results;
    }
}
