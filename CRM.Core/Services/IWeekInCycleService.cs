using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWeekInCycleService
    {
        Task<WeekInCycle> GetById(int id);
        Task<WeekInCycle> Create(WeekInCycle newWeekInCycle);
        Task<List<WeekInCycle>> CreateRange(List<WeekInCycle> newWeekInCycle);
        Task Update(WeekInCycle WeekInCycleToBeUpdated, WeekInCycle WeekInCycle);
        Task Delete(WeekInCycle WeekInCycleToBeDeleted);
        Task DeleteRange(List<WeekInCycle> WeekInCycle);

        Task<IEnumerable<WeekInCycle>> GetAll();
        Task<IEnumerable<WeekInCycle>> GetAllActif();
        Task<IEnumerable<WeekInCycle>> GetAllInActif();

    }
}
