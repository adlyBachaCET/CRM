using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ITargetService
    {
        Task<Target> GetById(int id);
        Task<Target> Create(Target newCycleSectorWeekDoctors);
        Task<List<Target>> CreateRange(List<Target> newCycleSectorWeekDoctors);
        Task Delete(Target CycleSectorWeekDoctorsToBeDeleted);
        Task DeleteRange(List<Target> CycleSectorWeekDoctors);
        Task<IEnumerable<Target>> GetByNumTarget(int id);
        Task<IEnumerable<Target>> GetAll();
        Task<IEnumerable<Target>> GetAllActif();
        Task<IEnumerable<Target>> GetAllInActif();

    }
}
