using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IDoctorService
    {
        Task<Doctor> GetById(int id);
        Task<Doctor> Create(Doctor newDoctor);
        Task<List<Doctor>> CreateRange(List<Doctor> newDoctor);
        Task Update(Doctor DoctorToBeUpdated, Doctor Doctor);
        Task Delete(Doctor DoctorToBeDeleted);
        Task DeleteRange(List<Doctor> Doctor);

        Task<IEnumerable<Doctor>> GetAll();
        Task<IEnumerable<Doctor>> GetAllActif();
        Task<IEnumerable<Doctor>> GetAllInActif();
        Task<IEnumerable<Doctor>> GetDoctorsAssigned();
        Task<IEnumerable<Doctor>> GetDoctorsNotAssigned();
    }
}
