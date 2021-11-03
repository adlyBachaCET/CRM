using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IBrickLocalityRepository : IRepository<BrickLocality>
    {
        Task<IEnumerable<BrickLocality>> GetAllActif();
        Task<IEnumerable<BrickLocality>> GetAllInActif();
        Task<BrickLocality> GetByIdActif(int id);
        Task<IEnumerable<BrickLocality>> GetAllAcceptedActif();
        Task<IEnumerable<BrickLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<BrickLocality>> GetAllPending();
        Task<IEnumerable<BrickLocality>> GetAllRejected();
    }
}
