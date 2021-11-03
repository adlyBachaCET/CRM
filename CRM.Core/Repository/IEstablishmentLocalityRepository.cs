using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentLocalityRepository : IRepository<EstablishmentLocality>
    {
        Task<IEnumerable<EstablishmentLocality>> GetAllActif();
        Task<IEnumerable<EstablishmentLocality>> GetAllInActif();
        Task<IEnumerable<EstablishmentLocality>> GetAllAcceptedActif();
        Task<IEnumerable<EstablishmentLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<EstablishmentLocality>> GetAllPending();
        Task<IEnumerable<EstablishmentLocality>> GetAllRejected();
        Task<EstablishmentLocality> GetByIdActif(int id);

    }

}
