using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ICycleRepository : IRepository<Cycle>
    {
        Task<IEnumerable<Cycle>> GetAllActif();
        Task<IEnumerable<Cycle>> GetAllInActif();
        Task<Cycle> GetByIdActif(int id);
        Task<IEnumerable<Cycle>> GetAllAcceptedActif();
        Task<IEnumerable<Cycle>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Cycle>> GetAllPending();
        Task<IEnumerable<Cycle>> GetAllRejected();
    }
}
