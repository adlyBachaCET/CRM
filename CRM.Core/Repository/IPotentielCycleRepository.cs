using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPotentielCycleRepository : IRepository<PotentielCycle>
    {
        Task<IEnumerable<PotentielCycle>> GetAllActif();
        Task<IEnumerable<PotentielCycle>> GetAllInActif();
        Task<IEnumerable<PotentielCycle>> GetAllAcceptedActif();
        Task<IEnumerable<PotentielCycle>> GetAllAcceptedInactifActif();
        Task<IEnumerable<PotentielCycle>> GetAllPending();
        Task<IEnumerable<PotentielCycle>> GetAllRejected();
        Task<PotentielCycle> GetByIdActif(int id);

    }
}
