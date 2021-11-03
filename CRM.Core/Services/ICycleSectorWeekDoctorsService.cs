using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ICycleSectorWeekDoctorsService
    {
        Task<CycleSectorWeekDoctors> GetById(int id);
        Task<CycleSectorWeekDoctors> Create(CycleSectorWeekDoctors newCycleSectorWeekDoctors);
        Task<List<CycleSectorWeekDoctors>> CreateRange(List<CycleSectorWeekDoctors> newCycleSectorWeekDoctors);
        Task Delete(CycleSectorWeekDoctors CycleSectorWeekDoctorsToBeDeleted);
        Task DeleteRange(List<CycleSectorWeekDoctors> CycleSectorWeekDoctors);

        Task<IEnumerable<CycleSectorWeekDoctors>> GetAll();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllActif();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllInActif();

    }
}
