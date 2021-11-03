using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPotentielRepository : IRepository<Potentiel>
    {
        Task<IEnumerable<Potentiel>> GetAllActif();
        Task<IEnumerable<Potentiel>> GetAllInActif();
        Task<IEnumerable<Potentiel>> GetAllAcceptedActif();
        Task<IEnumerable<Potentiel>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Potentiel>> GetAllPending();
        Task<IEnumerable<Potentiel>> GetAllRejected();
        Task<Potentiel> GetByIdActif(int id);

    }
}
