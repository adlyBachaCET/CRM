using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IRequestDoctorService
    {
        Task<RequestDoctor> GetById(int id);
        Task<RequestDoctor> Create(RequestDoctor newRequestDoctor);
        Task<List<RequestDoctor>> CreateRange(List<RequestDoctor> newRequestDoctor);
        Task Update(RequestDoctor RequestDoctorToBeUpdated, RequestDoctor RequestDoctor);
        Task Delete(RequestDoctor RequestDoctorToBeDeleted);
        Task DeleteRange(List<RequestDoctor> RequestDoctor);

        Task<IEnumerable<RequestDoctor>> GetAll();
        Task<IEnumerable<RequestDoctor>> GetAllActif();
        Task<IEnumerable<RequestDoctor>> GetAllInActif();
        Task<IEnumerable<RequestDoctor>> GetByIdDoctor(int id);
        Task<IEnumerable<RequestDoctor>> GetByIdActifDoctor(int Id);
        Task<IEnumerable<RequestDoctor>> GetByIdActifUser(int Id);

    }
}
