using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IServiceDoctorRepository : IRepository<ServiceDoctor>
    {
        Task<IEnumerable<ServiceDoctor>> GetAllActif();
        Task<IEnumerable<ServiceDoctor>> GetAllInActif();
        Task<IEnumerable<ServiceDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<ServiceDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ServiceDoctor>> GetAllPending();
        Task<IEnumerable<ServiceDoctor>> GetAllRejected();
        Task<ServiceDoctor> GetByIdActif(int id);

    }
}
