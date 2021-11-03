using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWeekSectorCycleInYearService
    {
        Task<WeekSectorCycleInYear> GetById(int id);
        Task<WeekSectorCycleInYear> Create(WeekSectorCycleInYear newWeekSectorCycleInYear);
        Task<List<WeekSectorCycleInYear>> CreateRange(List<WeekSectorCycleInYear> newWeekSectorCycleInYear);
        Task Delete(WeekSectorCycleInYear WeekSectorCycleInYearToBeDeleted);
        Task DeleteRange(List<WeekSectorCycleInYear> WeekSectorCycleInYear);

        Task<IEnumerable<WeekSectorCycleInYear>> GetAll();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllActif();
        Task<IEnumerable<WeekSectorCycleInYear>> GetAllInActif();

    }
}
