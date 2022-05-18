using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Students.AssignSubjects
{
    public class AssignStudentSubjectsCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(StudentId, "invalid staff Id")
                .IsValidCollection(SubjectsIds, "invalid subjects Ids")
                .Result;
        }

        public int StudentId { get; set; }
        public IEnumerable<int> SubjectsIds { get; set; }
    }
}
