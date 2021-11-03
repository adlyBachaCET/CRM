using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentUserRepository : IRepository<EstablishmentUser>
    {
        Task<IEnumerable<EstablishmentUser>> GetAllActif();
        Task<IEnumerable<EstablishmentUser>> GetAllInActif();
        Task<IEnumerable<EstablishmentUser>> GetAllAcceptedActif();
        Task<IEnumerable<EstablishmentUser>> GetAllAcceptedInactifActif();
        Task<IEnumerable<EstablishmentUser>> GetAllPending();
        Task<IEnumerable<EstablishmentUser>> GetAllRejected();
        Task<EstablishmentUser> GetByIdActif(int id);

    }
}
