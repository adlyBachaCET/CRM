using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IWeekInCycleRepository : IRepository<WeekInCycle>
    {
        Task<IEnumerable<WeekInCycle>> GetAllActif();
        Task<IEnumerable<WeekInCycle>> GetAllInActif();
        Task<IEnumerable<WeekInCycle>> GetAllAcceptedActif();
        Task<IEnumerable<WeekInCycle>> GetAllAcceptedInactifActif();
        Task<IEnumerable<WeekInCycle>> GetAllPending();
        Task<IEnumerable<WeekInCycle>> GetAllRejected();
        Task<WeekInCycle> GetByIdActif(int id);

    }
}
