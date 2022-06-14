using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Students.AssignSubjects
{
    public class AssignStudentSubjectsCommandHandler : CommandHandler<AssignStudentSubjectsCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(AssignStudentSubjectsCommand command, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();
            var student = await Context.StudentRepository.GetByIdAsync(command.StudentId);
            if (student == null) return OperationResult.Failed($"student with Id-{command.StudentId} not found");

            foreach (var subjectId in command.SubjectsIds)
            {
                var subject = await Context.SubjectRepository.GetByIdAsync(subjectId);
                if (subject == null) errors.Add($"subject with Id-{subjectId} not found");
                else student.OffersSubject(subject);
            }

            var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            student.LastModifiedBy = $"{currentUser.FirstName} {currentUser.LastName}";
            student.LastModified = DateTime.UtcNow;
            await Context.StudentRepository.UpdateAsync(student, student.Id);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to assign subject");
            return OperationResult.Successful(new CommandResponse(student.Id).NotifyInvalidItems(errors));
        }
    }
}
