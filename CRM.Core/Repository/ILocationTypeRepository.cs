using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ILocationTypeRepository : IRepository<LocationType>
    {
        Task<IEnumerable<LocationType>> GetAllActif();
        Task<IEnumerable<LocationType>> GetAllInActif();
        Task<IEnumerable<LocationType>> GetAllAcceptedActif();
        Task<IEnumerable<LocationType>> GetAllAcceptedInactifActif();
        Task<IEnumerable<LocationType>> GetAllPending();
        Task<LocationType> GetByIdActif(int id);

        Task<IEnumerable<LocationType>> GetAllRejected();
    }
}
