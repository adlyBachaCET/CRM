using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISectorCycleRepository : IRepository<SectorCycle>
    {
        Task<IEnumerable<SectorCycle>> GetAllActif();
        Task<IEnumerable<SectorCycle>> GetAllInActif();
        Task<IEnumerable<SectorCycle>> GetAllAcceptedActif();
        Task<IEnumerable<SectorCycle>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SectorCycle>> GetAllPending();
        Task<IEnumerable<SectorCycle>> GetAllRejected();
        Task<SectorCycle> GetByIdActif(int id);

    }
}
