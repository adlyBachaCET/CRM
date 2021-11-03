using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IDelegateManagerRepository : IRepository<DelegateManager>
    {
        Task<IEnumerable<DelegateManager>> GetAllActif();
        Task<IEnumerable<DelegateManager>> GetAllInActif();
        Task<DelegateManager> GetByIdActif(int id);
        Task<IEnumerable<DelegateManager>> GetAllAcceptedActif();
        Task<IEnumerable<DelegateManager>> GetAllAcceptedInactifActif();
        Task<IEnumerable<DelegateManager>> GetAllPending();
        Task<IEnumerable<DelegateManager>> GetAllRejected();
    }
}
