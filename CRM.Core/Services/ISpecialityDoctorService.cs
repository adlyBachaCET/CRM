using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISpecialityDoctorService
    {
        Task<SpecialityDoctor> GetById(int id);
        Task<SpecialityDoctor> Create(SpecialityDoctor newSpecialityDoctor);
        Task<List<SpecialityDoctor>> CreateRange(List<SpecialityDoctor> newSpecialityDoctor);
        Task Update(SpecialityDoctor SpecialityDoctorToBeUpdated, SpecialityDoctor SpecialityDoctor);
        Task Delete(SpecialityDoctor SpecialityDoctorToBeDeleted);
        Task DeleteRange(List<SpecialityDoctor> SpecialityDoctor);

        Task<IEnumerable<SpecialityDoctor>> GetAll();
        Task<IEnumerable<SpecialityDoctor>> GetAllActif();
        Task<IEnumerable<SpecialityDoctor>> GetAllInActif();

    }
}
