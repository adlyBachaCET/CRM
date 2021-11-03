using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IServiceDoctorService
    {
        Task<ServiceDoctor> GetById(int id);
        Task<ServiceDoctor> Create(ServiceDoctor newServiceDoctor);
        Task<List<ServiceDoctor>> CreateRange(List<ServiceDoctor> newServiceDoctor);
        Task Delete(ServiceDoctor ServiceDoctorToBeDeleted);
        Task DeleteRange(List<ServiceDoctor> ServiceDoctor);

        Task<IEnumerable<ServiceDoctor>> GetAll();
        Task<IEnumerable<ServiceDoctor>> GetAllActif();
        Task<IEnumerable<ServiceDoctor>> GetAllInActif();

    }
}
