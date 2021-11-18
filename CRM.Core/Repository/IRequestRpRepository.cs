using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IRequestRpRepository : IRepository<RequestRp>
    {
        Task<IEnumerable<RequestRp>> GetAllActif();
        Task<IEnumerable<RequestRp>> GetAllInActif();
        Task<RequestRp> GetByIdActif(int id);
        Task<IEnumerable<RequestRp>> GetAllAcceptedActif();
        Task<IEnumerable<RequestRp>> GetAllAcceptedInactifActif();
        Task<IEnumerable<RequestRp>> GetAllPending();
        Task<IEnumerable<RequestRp>> GetAllRejected();

    }
}
