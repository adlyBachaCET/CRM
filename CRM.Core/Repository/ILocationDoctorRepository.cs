using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ILocationDoctorRepository : IRepository<LocationDoctor>
    {
        Task<IEnumerable<LocationDoctor>> GetAllActif();
        Task<IEnumerable<LocationDoctor>> GetAllInActif();
        Task<LocationDoctor> GetByIdActif(int id, int IdLocation);
        Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<LocationDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<LocationDoctor>> GetAllPending();
        Task<IEnumerable<LocationDoctor>> GetAllRejected();
        Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif(int Id);
    }
}
