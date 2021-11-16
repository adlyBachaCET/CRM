using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IServiceService
    {
        Task<Service> GetById(int? id);
        Task<Service> Create(Service newService);
        Task<List<Service>> CreateRange(List<Service> newService);
        Task Update(Service ServiceToBeUpdated, Service Service);
        Task Delete(Service ServiceToBeDeleted);
        Task DeleteRange(List<Service> Service);

        Task<IEnumerable<Service>> GetAll();
        Task<IEnumerable<Service>> GetAllActif();
        Task<IEnumerable<Service>> GetAllInActif();
        Task<Service> GetByNameActif(string Name);


    }
}
