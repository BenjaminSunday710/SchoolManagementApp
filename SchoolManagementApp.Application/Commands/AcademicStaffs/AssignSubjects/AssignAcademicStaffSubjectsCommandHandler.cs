﻿using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.AssignSubjects
{
    public class AssignAcademicStaffSubjectsCommandHandler : CommandHandler<AssignAcademicStaffSubjectsCommand, CoreDbContext, CommandResponse>
    {
        private IUserIdentity currentUser;

        public AssignAcademicStaffSubjectsCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
        public async override Task<ActionResult<CommandResponse>> HandleAsync(AssignAcademicStaffSubjectsCommand command, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();
            var teacher = await Context.AcademicStaffRepository.GetByIdAsync(command.StaffId);
            if (teacher == null) return OperationResult.Failed($"Staff with Id-{command.StaffId} not found");

            foreach (var subjectId in command.SubjectsIds)
            {
                var subject = await Context.SubjectRepository.GetByIdAsync(subjectId);
                if (subject == null) errors.Add($"subject with Id-{subjectId} not found");
                else teacher.AssignSubject(subject);
            }

            //var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            teacher.LastModifiedBy = $"{currentUser.FirstName} {currentUser.LastName}";
            teacher.LastModified = DateTime.UtcNow;
            await Context.AcademicStaffRepository.UpdateAsync(teacher, teacher.Id);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to assign subject");
            return OperationResult.Successful(new CommandResponse(teacher.Id)).SetErrors(errors);
        }
    }
}
