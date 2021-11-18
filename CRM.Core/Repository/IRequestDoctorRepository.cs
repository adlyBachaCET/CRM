using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IRequestDoctorRepository : IRepository<RequestDoctor>
    {
        Task<IEnumerable<RequestDoctor>> GetAllActif();
        Task<IEnumerable<RequestDoctor>> GetAllInActif();
        Task<IEnumerable<RequestDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<RequestDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<RequestDoctor>> GetAllPending();
        Task<IEnumerable<RequestDoctor>> GetAllRejected();
        Task<RequestDoctor> GetByIdActif(int id);
        Task<IEnumerable<RequestDoctor>> GetByIdDoctor(int id);
        Task<IEnumerable<RequestDoctor>> GetByIdActifDoctor(int Id);
        Task<IEnumerable<RequestDoctor>> GetByIdActifUser(int Id);

    }
}
