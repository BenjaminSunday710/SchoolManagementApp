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
            SchoolRepository = new Repository<School, int>(session);
            SchoolClassRepository = new Repository<SchoolClass, int>(session);
            SubjectRepository = new Repository<Subject, int>(session);
            StudentRepository = new Repository<Student, int>(session);
            StudentSubjectRepository = new Repository<StudentSubject, int>(session);
            AcademicStaffRepository = new Repository<AcademicStaff, int>(session);
            AcademicStaffSubjectRepository = new Repository<AcademicStaffSubject, int>(session);
            NonAcademicStaffRepository = new Repository<NonAcademicStaff, int>(session);
            ResultRepository = new Repository<Result, int>(session);
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

        public IRepository<School,int> SchoolRepository { get; private set; }
        public IRepository<SchoolClass,int> SchoolClassRepository { get; private set; }
        public IRepository<Subject,int> SubjectRepository { get; private set; }
        public IRepository<Student,int> StudentRepository { get; private set; }
        public IRepository<StudentSubject,int> StudentSubjectRepository { get; private set; }
        public IRepository<AcademicStaff,int> AcademicStaffRepository { get; private set; }
        public IRepository<AcademicStaffSubject,int> AcademicStaffSubjectRepository { get; private set; }
        public IRepository<NonAcademicStaff,int> NonAcademicStaffRepository { get; private set; }
        public IRepository<Result,int> ResultRepository { get; private set; }
       
    }
}
