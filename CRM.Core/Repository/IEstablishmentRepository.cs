using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IEstablishmentRepository : IRepository<Establishment>
    {
        Task<IEnumerable<Establishment>> GetAllActif();
        Task<IEnumerable<Establishment>> GetAllInActif();
        Task<IEnumerable<Establishment>> GetAllAcceptedActif();
        Task<IEnumerable<Establishment>> GetAllAcceptedInactifActif();
        Task<Establishment> GetByIdActif(int id);

        Task<IEnumerable<Establishment>> GetAllPending();
        Task<IEnumerable<Establishment>> GetAllRejected();
    }
}
