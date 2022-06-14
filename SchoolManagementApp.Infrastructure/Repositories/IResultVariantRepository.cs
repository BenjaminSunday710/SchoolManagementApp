using SchoolManagementApp.Domain.Results;
using System.Threading.Tasks;

namespace SchoolManagementApp.Infrastructure.Repositories
{
    public interface IResultVariantRepository
    {
        Task<ResultVariantManager> GetResultVariantManager(string session, Term term);
    }
}
