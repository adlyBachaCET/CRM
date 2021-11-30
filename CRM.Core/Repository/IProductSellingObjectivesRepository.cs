using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IProductSellingObjectivesRepository : IRepository<ProductSellingObjectives>
    {
        Task<IEnumerable<ProductSellingObjectives>> GetAllActif();
        Task<IEnumerable<ProductSellingObjectives>> GetAllInActif();
        Task<IEnumerable<ProductSellingObjectives>> GetAllAcceptedActif();
        Task<IEnumerable<ProductSellingObjectives>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ProductSellingObjectives>> GetAllPending();
        Task<IEnumerable<ProductSellingObjectives>> GetAllRejected();
        Task<ProductSellingObjectives> GetByIdActif(int id);

        Task<IEnumerable<ProductSellingObjectives>> GetByIdSellingObjectives(int id);

    }
}
