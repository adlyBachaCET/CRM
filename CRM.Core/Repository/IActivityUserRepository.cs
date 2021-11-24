using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IActivityUserRepository : IRepository<ActivityUser>
    {
        Task<IEnumerable<ActivityUser>> GetAllActif();
        Task<IEnumerable<ActivityUser>> GetAllInActif();
        Task<IEnumerable<ActivityUser>> GetAllAcceptedActif();
        Task<IEnumerable<ActivityUser>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ActivityUser>> GetAllPending();
        Task<IEnumerable<ActivityUser>> GetAllRejected();
        Task<ActivityUser> GetByIdActif(int id);
        Task<IEnumerable<ActivityUser>> GetAllById(int Id);

    }
}
