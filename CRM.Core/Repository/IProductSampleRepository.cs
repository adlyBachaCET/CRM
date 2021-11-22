using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IProductSampleRepository : IRepository<ProductSample>
    {
        Task<IEnumerable<ProductSample>> GetAllActif();
        Task<IEnumerable<ProductSample>> GetAllInActif();
        Task<IEnumerable<ProductSample>> GetAllAcceptedActif();
        Task<IEnumerable<ProductSample>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ProductSample>> GetAllPending();
        Task<IEnumerable<ProductSample>> GetAllRejected();
        Task<ProductSample> GetByIdActif(int id);

        Task<IEnumerable<ProductSample>> GetByIdPharmacy(int id);

    }
}
