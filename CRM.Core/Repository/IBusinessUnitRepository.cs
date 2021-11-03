using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IBusinessUnitRepository : IRepository<BusinessUnit>
    {
        Task<IEnumerable<BusinessUnit>> GetAllActif();
        Task<IEnumerable<BusinessUnit>> GetAllInActif();
        Task<BusinessUnit> GetByIdActif(int id);
        Task<IEnumerable<BusinessUnit>> GetAllAcceptedActif();
        Task<IEnumerable<BusinessUnit>> GetAllAcceptedInactifActif();
        Task<IEnumerable<BusinessUnit>> GetAllPending();
        Task<IEnumerable<BusinessUnit>> GetAllRejected();

    }
}
