using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISectorCycleService
    {
        Task<SectorCycle> GetById(int id);
        Task<SectorCycle> Create(SectorCycle newWeekSectorCycle);
        Task<List<SectorCycle>> CreateRange(List<SectorCycle> newWeekSectorCycle);
        Task Delete(SectorCycle WeekSectorCycleToBeDeleted);
        Task DeleteRange(List<SectorCycle> WeekSectorCycle);

        Task<IEnumerable<SectorCycle>> GetAll();
        Task<IEnumerable<SectorCycle>> GetAllActif();
        Task<IEnumerable<SectorCycle>> GetAllInActif();

    }
}
