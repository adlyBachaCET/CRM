using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISpecialtyService
    {
        Task<Specialty> GetById(int id);
        Task<Specialty> Create(Specialty newSpecialty);
        Task<List<Specialty>> CreateRange(List<Specialty> newSpecialty);
        Task Update(Specialty SpecialtyToBeUpdated, Specialty Specialty);
        Task Delete(Specialty SpecialtyToBeDeleted);
        Task DeleteRange(List<Specialty> Specialty);

        Task<IEnumerable<Specialty>> GetAll();
        Task<IEnumerable<Specialty>> GetAllActif();
        Task<IEnumerable<Specialty>> GetAllInActif();

    }
}
