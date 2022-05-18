using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.AssignSubjects
{
    public class AssignAcademicStaffSubjectsCommandHandler : CommandHandler<AssignAcademicStaffSubjectsCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(AssignAcademicStaffSubjectsCommand command, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();
            var teacher = await Context.AcademicStaffRepository.GetByIdAsync(command.StaffId);
            if (teacher == null) return OperationResult.Failed($"Staff with Id-{command.StaffId} not found");

            foreach (var subjectId in command.SubjectsIds)
            {
                var subject = await Context.SubjectRepository.GetByIdAsync(subjectId);
                if (subject == null) errors.Add($"subject with Id-{subjectId} not found");
                else
                {
                    var staffSubject = new AcademicStaffSubject(subject, teacher);
                    await Context.AcademicStaffSubjectRepository.AddAsync(staffSubject);
                    await Context.SubjectRepository.UpdateAsync(subject, subject.Id);
                }
            }
            await Context.AcademicStaffRepository.UpdateAsync(teacher, teacher.Id);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to assign subject");
            return OperationResult.Successful(new CommandResponse(teacher.Id)).SetErrors(errors);
        }
    }
}
