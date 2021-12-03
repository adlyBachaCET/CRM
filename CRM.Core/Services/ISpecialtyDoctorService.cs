using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISpecialtyDoctorService
    {
        Task<SpecialtyDoctor> GetById(int id);
        Task<SpecialtyDoctor> GetById(int idDoctor, int idSpecialty);

        Task<SpecialtyDoctor> Create(SpecialtyDoctor newSpecialtyDoctor);
        Task<List<SpecialtyDoctor>> CreateRange(List<SpecialtyDoctor> newSpecialtyDoctor);
        Task Delete(SpecialtyDoctor SpecialtyDoctorToBeDeleted);
        Task DeleteRange(List<SpecialtyDoctor> SpecialtyDoctor);
        Task<IEnumerable<SpecialtyDoctor>> GetByIdDoctor(int id);

        Task<IEnumerable<SpecialtyDoctor>> GetAll();
        Task<IEnumerable<SpecialtyDoctor>> GetAllActif();
        Task<IEnumerable<SpecialtyDoctor>> GetAllInActif();

    }
}
