using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPharmacyLocalityRepository : IRepository<PharmacyLocality>
    {
        Task<IEnumerable<PharmacyLocality>> GetAllActif();
        Task<IEnumerable<PharmacyLocality>> GetAllInActif();
        Task<IEnumerable<PharmacyLocality>> GetAllAcceptedActif();
        Task<IEnumerable<PharmacyLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<PharmacyLocality>> GetAllPending();
        Task<IEnumerable<PharmacyLocality>> GetAllRejected();
        Task<PharmacyLocality> GetByIdActif(int id);

    }
}
