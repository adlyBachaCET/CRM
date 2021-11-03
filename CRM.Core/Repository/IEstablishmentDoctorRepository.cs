using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentDoctorRepository : IRepository<EstablishmentDoctor>
    {
        Task<IEnumerable<EstablishmentDoctor>> GetAllActif();
        Task<IEnumerable<EstablishmentDoctor>> GetAllInActif();
        Task<EstablishmentDoctor> GetByIdActif(int id);
        Task<IEnumerable<EstablishmentDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<EstablishmentDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<EstablishmentDoctor>> GetAllPending();
        Task<IEnumerable<EstablishmentDoctor>> GetAllRejected();
    }
}
