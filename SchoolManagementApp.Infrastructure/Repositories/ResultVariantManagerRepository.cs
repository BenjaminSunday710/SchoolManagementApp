using NHibernate;
using NHibernate.Linq;
using SchoolManagementApp.Domain.Results;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace SchoolManagementApp.Infrastructure.Repositories
{
    public class ResultVariantManagerRepository : Repository<ResultVariantManager,Guid>, IResultVariantRepository
    {
        public ResultVariantManagerRepository(ISession session):base(session)
        {
            _session = session;
        }

        public async Task<ResultVariantManager> GetResultVariantManager(string session, Term term)
        {
            return await _session.Query<ResultVariantManager>().FirstOrDefaultAsync(x => x.Session == session && x.Term == term);
        }

        private ISession _session;
    }
}
