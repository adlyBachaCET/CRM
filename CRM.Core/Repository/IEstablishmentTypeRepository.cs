using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentTypeRepository : IRepository<EstablishmentType>
    {
        Task<IEnumerable<EstablishmentType>> GetAllActif();
        Task<IEnumerable<EstablishmentType>> GetAllInActif();
        Task<IEnumerable<EstablishmentType>> GetAllAcceptedActif();
        Task<IEnumerable<EstablishmentType>> GetAllAcceptedInactifActif();
        Task<IEnumerable<EstablishmentType>> GetAllPending();
        Task<EstablishmentType> GetByIdActif(int id);

        Task<IEnumerable<EstablishmentType>> GetAllRejected();
    }
}
