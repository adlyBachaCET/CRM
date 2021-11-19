using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISupportRepository : IRepository<Support>
    {
        Task<IEnumerable<Support>> GetAllActif();
        Task<IEnumerable<Support>> GetAllInActif();
        Task<IEnumerable<Support>> GetAllAcceptedActif();
        Task<IEnumerable<Support>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Support>> GetAllPending();
        Task<IEnumerable<Support>> GetAllRejected();
        Task<Support> GetByIdActif(int? id);
        Task<Support> GetByNameActif(string Name);

    }
}
