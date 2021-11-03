
using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWeekInYearService
    {
        Task<WeekInYear> GetById(int ordre, int year);
        Task<WeekInYear> Create(WeekInYear newWeekInYear);
        Task<List<WeekInYear>> CreateRange(List<WeekInYear> newWeekInYear);
        Task Update(WeekInYear WeekInYearToBeUpdated, WeekInYear WeekInYear);
        Task Delete(WeekInYear WeekInYearToBeDeleted);
        Task DeleteRange(List<WeekInYear> WeekInYear);

        Task<IEnumerable<WeekInYear>> GetAll();
        Task<IEnumerable<WeekInYear>> GetAllActif();
        Task<IEnumerable<WeekInYear>> GetAllInActif();

    }
}
