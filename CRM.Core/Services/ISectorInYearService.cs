
using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISectorInYearService
    {
        Task<SectorInYear> GetById(int id);
        Task<SectorInYear> Create(SectorInYear newWeekSectorCycleInYear);
        Task<List<SectorInYear>> CreateRange(List<SectorInYear> newWeekSectorCycleInYear);
        Task Delete(SectorInYear WeekSectorCycleInYearToBeDeleted);
        Task DeleteRange(List<SectorInYear> WeekSectorCycleInYear);

        Task<IEnumerable<SectorInYear>> GetAll();
        Task<IEnumerable<SectorInYear>> GetAllActif();
        Task<IEnumerable<SectorInYear>> GetAllInActif();
        Task RequestOpeningWeek(int IdSector);
        Task DenyRequestOpeningWeek(int IdSector);
    }
}
