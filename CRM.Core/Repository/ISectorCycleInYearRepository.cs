using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISectorCycleInYearRepository : IRepository<SectorCycleInYear>
    {
        Task<IEnumerable<SectorCycleInYear>> GetAllActif();
        Task<IEnumerable<SectorCycleInYear>> GetAllInActif();
        Task<IEnumerable<SectorCycleInYear>> GetAllAcceptedActif();
        Task<IEnumerable<SectorCycleInYear>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SectorCycleInYear>> GetAllPending();
        Task<IEnumerable<SectorCycleInYear>> GetAllRejected();
        Task<SectorCycleInYear> GetByIdActif(int id);

    }
}
