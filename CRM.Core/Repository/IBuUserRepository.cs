using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IBuUserRepository : IRepository<BuUser>
    {
        Task<BuUser> GetByIdUser(int id);
        Task<IEnumerable<BuUser>> GetAllActif();
        Task<IEnumerable<BuUser>> GetAllInActif();
        Task<BuUser> GetByIdActif(int id);
        Task<IEnumerable<BuUser>> GetAllAcceptedActif();
        Task<IEnumerable<BuUser>> GetAllAcceptedInactifActif();
        Task<IEnumerable<BuUser>> GetAllPending();
        Task<IEnumerable<BuUser>> GetAllRejected();
    }
}
