﻿using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Domain.Subjects;
using Shared.Domain.Entities;
using System;

namespace SchoolManagementApp.Domain.Results
{
    public class Result:BaseEntity<Guid>
    {
        protected internal Result() { }

        public virtual void AssignStudent(Student student)
        {
            student.HasResult(this);
        }

        public virtual void AssignSubject(Subject subject)
        {
            subject.HasResult(this);
        }
        
        public virtual void AssignSchoolClass(SchoolClass schoolClass)
        {
            schoolClass.HasResult(this);
        }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
        public virtual ResultVariantManager ResultVariantManager { get; set; }
        public virtual double ContinuousAssessment { get; set; }
        public virtual double Examination { get; set; }
        public virtual double Total { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Remark Remark { get; set; }
    }
}
