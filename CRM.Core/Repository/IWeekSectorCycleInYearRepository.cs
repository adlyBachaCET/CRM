using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IWeekSectorCycleInYearRepository : IRepository<WeekSectorCycleInYear>
    {
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllActif();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllInActif();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllAcceptedActif();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllAcceptedInactifActif();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllPending();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllRejected();
        Task<WeekSectorCycleInYear> GetByIdActif(int id);

    }
}
