using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ILocalityRepository : IRepository<Locality>
    {
        Task<IEnumerable<Locality>> GetAllActif();
        Task<IEnumerable<Locality>> GetAllInActif();
        Task<IEnumerable<Locality>> GetAllAcceptedActif();
        Task<IEnumerable<Locality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Locality>> GetAllPending();
        Task<IEnumerable<Locality>> GetAllRejected();
        Task<Locality> GetByIdActif(int id);

    }
}
