using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISectorCycleInYearService
    {
        Task<SectorCycleInYear> GetById(int id);
        Task<SectorCycleInYear> Create(SectorCycleInYear newWeekSectorCycleInYear);
        Task<List<SectorCycleInYear>> CreateRange(List<SectorCycleInYear> newWeekSectorCycleInYear);
        Task Delete(SectorCycleInYear WeekSectorCycleInYearToBeDeleted);
        Task DeleteRange(List<SectorCycleInYear> WeekSectorCycleInYear);

        Task<IEnumerable<SectorCycleInYear>> GetAll();
        Task<IEnumerable<SectorCycleInYear>> GetAllActif();
        Task<IEnumerable<SectorCycleInYear>> GetAllInActif();

    }
}
