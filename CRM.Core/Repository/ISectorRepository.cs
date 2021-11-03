using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISectorRepository : IRepository<Sector>
    {
        Task<IEnumerable<Sector>> GetAllActif();
        Task<IEnumerable<Sector>> GetAllInActif();
        Task<IEnumerable<Sector>> GetAllAcceptedActif();
        Task<IEnumerable<Sector>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Sector>> GetAllPending();
        Task<IEnumerable<Sector>> GetAllRejected();
        Task<Sector> GetByIdActif(int id);

    }
}
