using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetAllActif();
        Task<IEnumerable<Location>> GetAllInActif();
        Task<IEnumerable<Location>> GetAllAcceptedActif();
        Task<IEnumerable<Location>> GetAllAcceptedInactifActif();
        Task<Location> GetByIdActif(int id);

        Task<IEnumerable<Location>> GetAllPending();
        Task<IEnumerable<Location>> GetAllRejected();
    }
}
