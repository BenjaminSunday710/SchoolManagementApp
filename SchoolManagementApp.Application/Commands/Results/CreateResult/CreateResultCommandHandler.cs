using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Results.CreateResult
{
    public class CreateResultCommandHandler : CommandHandler<CreateResultCommand, CoreDbContext, CommandResponse>
    {
        private IUserIdentity currentUser;

        public CreateResultCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
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
            var result = resultBuilder.Build();
            result.AssignSchoolClass(schoolClass);
            result.AssignStudent(student);
            result.AssignSubject(subject);

            //var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            result.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            var resultVariantManager = await Context.ResultVariantManagerRepository.GetResultVariantManager(command.Session, command.Term);
            if (resultVariantManager == null) await CreateResultVariantManager(result, Context, command.Session, command.Term);
            else resultVariantManager.AddResult(result);

            await Context.ResultRepository.AddAsync(result);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to save result");
            return OperationResult.Successful(new CommandResponse(result.Id));
        }

        private async Task CreateResultVariantManager(Result result, CoreDbContext context, string session, Term term)
        {
            var resultVariantManager = new ResultVariantManager(session, term);
            resultVariantManager.AddResult(result);
            resultVariantManager.CreatedBy = result.CreatedBy;
            await Context.ResultVariantManagerRepository.AddAsync(resultVariantManager);
        }

    }
}
