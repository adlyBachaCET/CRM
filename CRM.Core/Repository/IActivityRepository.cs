using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetAllActif();
        Task<IEnumerable<Activity>> GetAllInActif();
        Task<IEnumerable<Activity>> GetAllAcceptedActif();
        Task<IEnumerable<Activity>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Activity>> GetAllPending();
        Task<IEnumerable<Activity>> GetAllRejected();
        Task<Activity> GetByIdActif(int id);
        Task<List<Activity>> GetByIdUser(int id);
        Task<IEnumerable<Activity>> GetAllById(int Id);
        Task<List<Activity>> GetByIdUserByToday(int id);
    }
}
