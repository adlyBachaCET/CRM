using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetAllActif();
        Task<IEnumerable<Service>> GetAllInActif();
        Task<IEnumerable<Service>> GetAllAcceptedActif();
        Task<IEnumerable<Service>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Service>> GetAllPending();
        Task<IEnumerable<Service>> GetAllRejected();
        Task<Service> GetByIdActif(int? id);
        Task<Service> GetByNameActif(string Name);

    }
}
