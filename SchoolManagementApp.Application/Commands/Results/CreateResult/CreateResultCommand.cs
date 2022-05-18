using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Results.CreateResult
{
    public class CreateResultCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(StudentId, "invalid student Id")
                .IsValidInt(SubjectId, "invalid subject Id")
                .IsValidInt(SchoolClassId, "invalid school class Id")
                .IsValidText(Session, "invalid session")
                .IsValidText(Term.ToString(), "invalid term")
                .IsValidDouble(ContinuousAssessment, "invalid Continuous Assessment score")
                .IsValidDouble(Examination, "invalid Examination score")
                .IsValidText(Grade.ToString(), "invalid grade")
                .IsValidText(Remark.ToString(), "invalid remark")
                .Result;
        }

        public  int StudentId { get; set; }
        public  int SubjectId { get; set; }
        public  int SchoolClassId { get; set; }
        public  string Session { get; set; }
        public  Term Term { get; set; }
        public  double ContinuousAssessment { get; set; }
        public  double Examination { get; set; }
        public  Grade Grade { get; set; }
        public  Remark Remark { get; set; }
    }
}
