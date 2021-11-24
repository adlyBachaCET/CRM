using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPotentielSectorRepository : IRepository<PotentielSector>
    {
        Task<IEnumerable<PotentielSector>> GetAllActif();
        Task<IEnumerable<PotentielSector>> GetAllInActif();
        Task<IEnumerable<PotentielSector>> GetAllAcceptedActif();
        Task<IEnumerable<PotentielSector>> GetAllAcceptedInactifActif();
        Task<IEnumerable<PotentielSector>> GetAllPending();
        Task<IEnumerable<PotentielSector>> GetAllRejected();
        Task<PotentielSector> GetByIdActif(int id);

    }
}
