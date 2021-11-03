using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWeekSectorCycleService
    {
        Task<WeekSectorCycle> GetById(int id);
        Task<WeekSectorCycle> Create(WeekSectorCycle newWeekSectorCycle);
        Task<List<WeekSectorCycle>> CreateRange(List<WeekSectorCycle> newWeekSectorCycle);
        Task Delete(WeekSectorCycle WeekSectorCycleToBeDeleted);
        Task DeleteRange(List<WeekSectorCycle> WeekSectorCycle);

        Task<IEnumerable<WeekSectorCycle>> GetAll();
        Task<IEnumerable<WeekSectorCycle>> GetAllActif();
        Task<IEnumerable<WeekSectorCycle>> GetAllInActif();

    }
}
