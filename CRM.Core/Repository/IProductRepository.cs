using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllActif();
        Task<IEnumerable<Product>> GetAllInActif();
        Task<IEnumerable<Product>> GetAllAcceptedActif();
        Task<IEnumerable<Product>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Product>> GetAllPending();
        Task<IEnumerable<Product>> GetAllRejected();
        Task<Product> GetByIdActif(int id);

        Task<IEnumerable<Product>> GetByIdPharmacy(int id);

    }
}
