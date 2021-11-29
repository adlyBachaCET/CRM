using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISectorInYearRepository : IRepository<SectorInYear>
    {
        Task<IEnumerable<SectorInYear>> GetAllActif();
        Task<IEnumerable<SectorInYear>> GetAllInActif();
        Task<IEnumerable<SectorInYear>> GetAllAcceptedActif();
        Task<IEnumerable<SectorInYear>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SectorInYear>> GetAllPending();
        Task<IEnumerable<SectorInYear>> GetAllRejected();
        Task<SectorInYear> GetByIdActif(int id);

    }
}
