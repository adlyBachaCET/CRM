using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ITargetRepository : IRepository<Target>
    {
        Task<IEnumerable<Target>> GetByFullTarget(int id);
        Task<IEnumerable<Target>> GetEmptyTargetByNumTarget(int id);
        Task<IEnumerable<Target>> GetAllActif();
        Task<IEnumerable<Target>> GetAllInActif();
        Task<Target> GetByIdActif(int id);
        Task<IEnumerable<Target>> GetAllAcceptedActif();
        Task<IEnumerable<Target>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Target>> GetAllPending();
        Task<IEnumerable<Target>> GetAllRejected();
    }
}
