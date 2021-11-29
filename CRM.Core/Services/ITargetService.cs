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
        Task<IEnumerable<Target>> GetTargetsByIdUser(int id);
        Task<IEnumerable<Doctor>> GetDoctorsByNumTarget(int NumTarget, int IdSector);
        Task<IEnumerable<Pharmacy>> GetPharmacysByNumTarget(int NumTarget, int IdSector);
        Task<Cycle> GetCycleByNumTarget(int id);
        Task<IEnumerable<Target>> GetAllActif();
        Task<IEnumerable<Target>> GetAllInActif();
        Task<IEnumerable<Sector>> GetSectorsByNumTarget(int id);
        Task<IEnumerable<Sector>> GetFullSectorsByNumTarget(int id);
        Task WeekSwap(WeekSwap WeekSwap);
        Task DeleteWeek(WeekDeletion WeekSwap);
        Task UpdateWeek(WeekUpdate WeekUpdate);
        Task<User> GetUserByNumTarget(int id);
        Task RemoveAll();
    }
}
