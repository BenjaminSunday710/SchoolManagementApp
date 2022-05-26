using NHibernate;
using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Domain.Subjects;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Infrastructure.Context
{
    public class CoreDbContext:DbContext
    {
        private ISession _session;
        public CoreDbContext() { }

        public override void Setup(ISession session)
        {
            _session = session;
            SchoolRepository = new Repository<School, Guid>(session);
            SchoolClassRepository = new Repository<SchoolClass, Guid>(session);
            SubjectRepository = new Repository<Subject, Guid>(session);
            StudentRepository = new Repository<Student, Guid>(session);
            AcademicStaffRepository = new Repository<AcademicStaff, Guid>(session);
            NonAcademicStaffRepository = new Repository<NonAcademicStaff, Guid>(session);
            ResultRepository = new Repository<Result, Guid>(session);
        }

        public async override Task<ActionResult> CommitAsync()
        {
            try
            {
                using (var transaction = _session.BeginTransaction())
                {
                    await transaction.CommitAsync();
                    return ActionResult.Success();
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        public IRepository<School, Guid> SchoolRepository { get; private set; }
        public IRepository<SchoolClass, Guid> SchoolClassRepository { get; private set; }
        public IRepository<Subject, Guid> SubjectRepository { get; private set; }
        public IRepository<Student, Guid> StudentRepository { get; private set; }
        public IRepository<AcademicStaff, Guid> AcademicStaffRepository { get; private set; }
        public IRepository<NonAcademicStaff, Guid> NonAcademicStaffRepository { get; private set; }
        public IRepository<Result, Guid> ResultRepository { get; private set; }
       
    }
}
