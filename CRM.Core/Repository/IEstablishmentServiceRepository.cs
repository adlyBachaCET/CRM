using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentServiceRepository : IRepository<EstablishmentService>
    {
        Task<IEnumerable<EstablishmentService>> GetAllActif();
        Task<IEnumerable<EstablishmentService>> GetAllInActif();
        Task<IEnumerable<EstablishmentService>> GetAllAcceptedActif();
        Task<IEnumerable<EstablishmentService>> GetAllAcceptedInactifActif(); 
        Task<EstablishmentService> GetByIdActif(int id);

        Task<IEnumerable<EstablishmentService>> GetAllPending();
        Task<IEnumerable<EstablishmentService>> GetAllRejected();
    }
}
