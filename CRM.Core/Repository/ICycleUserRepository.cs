using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ICycleUserRepository : IRepository<CycleUser>
    {
        Task<CycleUser> GetByIdCycleUser(int idCycle, int idUser);
        Task<List<Cycle>> GetByIdUser(int id);
        Task<IEnumerable<CycleUser>> GetAllActif();
        Task<IEnumerable<CycleUser>> GetAllInActif();
        Task<CycleUser> GetByIdActif(int id);
        Task<IEnumerable<CycleUser>> GetAllAcceptedActif();
        Task<IEnumerable<CycleUser>> GetAllAcceptedInactifActif();
        Task<IEnumerable<CycleUser>> GetAllPending();
        Task<IEnumerable<CycleUser>> GetAllRejected();
    }
}
