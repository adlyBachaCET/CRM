using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISectorLocalityRepository : IRepository<SectorLocality>
    {
        Task<IEnumerable<SectorLocality>> GetAllActif();
        Task<IEnumerable<SectorLocality>> GetAllInActif();
        Task<IEnumerable<SectorLocality>> GetAllAcceptedActif();
        Task<IEnumerable<SectorLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SectorLocality>> GetAllPending();
        Task<IEnumerable<SectorLocality>> GetAllRejected();
        Task<SectorLocality> GetByIdActif(int id);

    }
}
