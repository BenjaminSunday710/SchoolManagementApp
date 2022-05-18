using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Results.CreateResult
{
    public class CreateResultCommandHandler : CommandHandler<CreateResultCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateResultCommand command, CancellationToken cancellationToken = default)
        {
            var student = await Context.StudentRepository.GetByIdAsync(command.StudentId);
            if (student == null) return OperationResult.Failed($"student with Id-{command.StudentId} not found");

            var schoolClass = await Context.SchoolClassRepository.GetByIdAsync(command.SchoolClassId);
            if (schoolClass == null) return OperationResult.Failed($"student with Id-{command.SchoolClassId} not found");

            var subject = await Context.SubjectRepository.GetByIdAsync(command.SubjectId);
            if (subject == null) return OperationResult.Failed($"student with Id-{command.SubjectId} not found");

            var resultBuilder = new ResultBuilder();
            resultBuilder.SetContinuousAssessmentScore(command.ContinuousAssessment);
            resultBuilder.SetExamScore(command.Examination);
            resultBuilder.SetGrade(command.Grade);
            resultBuilder.SetRemark(command.Remark);
            resultBuilder.SetSession(command.Session);
            resultBuilder.SetTerm(command.Term);
            var result = resultBuilder.Build();
            result.AssignSchoolClass(schoolClass);
            result.AssignStudent(student);
            result.AssignSubject(subject);

            await Context.ResultRepository.AddAsync(result);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to save result");
            return OperationResult.Successful(new CommandResponse(result.Id));
             
        }
    }
}
