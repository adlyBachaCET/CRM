using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ICycleBuRepository : IRepository<CycleBu>
    {
        Task<IEnumerable<CycleBu>> GetAllActif();
        Task<IEnumerable<CycleBu>> GetAllInActif();
        Task<CycleBu> GetByIdActif(int id);
        Task<IEnumerable<CycleBu>> GetAllAcceptedActif();
        Task<IEnumerable<CycleBu>> GetAllAcceptedInactifActif();
        Task<IEnumerable<CycleBu>> GetAllPending();
        Task<IEnumerable<CycleBu>> GetAllRejected();
    }
}
