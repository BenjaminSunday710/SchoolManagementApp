using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Results.CreateResult
{
    public class CreateResultCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StudentId, "invalid student Id")
                .IsValidGuid(SubjectId, "invalid subject Id")
                .IsValidGuid(SchoolClassId, "invalid school class Id")
                .IsValidText(Session, "invalid session")
                .IsValidText(Term.ToString(), "invalid term")
                .IsValidDouble(ContinuousAssessment, "invalid Continuous Assessment score")
                .IsValidDouble(Examination, "invalid Examination score")
                .IsValidText(Grade.ToString(), "invalid grade")
                .IsValidText(Remark.ToString(), "invalid remark")
                .Result;
        }

        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolClassId { get; set; }
        public  string Session { get; set; }
        public  Term Term { get; set; }
        public  double ContinuousAssessment { get; set; }
        public  double Examination { get; set; }
        public  Grade Grade { get; set; }
        public  Remark Remark { get; set; }
    }
}
