using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IProductPharmacyRepository : IRepository<ProductPharmacy>
    {
        Task<IEnumerable<ProductPharmacy>> GetAllActif();
        Task<IEnumerable<ProductPharmacy>> GetAllInActif();
        Task<IEnumerable<ProductPharmacy>> GetAllAcceptedActif();
        Task<IEnumerable<ProductPharmacy>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ProductPharmacy>> GetAllPending();
        Task<IEnumerable<ProductPharmacy>> GetAllRejected();
        Task<ProductPharmacy> GetByIdActif(int id);

        Task<IEnumerable<ProductPharmacy>> GetByIdPharmacy(int id);

    }
}
