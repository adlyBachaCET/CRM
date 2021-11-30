using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISellingObjectivesRepository : IRepository<SellingObjectives>
    {
        Task<IEnumerable<SellingObjectives>> GetAllActif();
        Task<IEnumerable<SellingObjectives>> GetAllInActif();
        Task<IEnumerable<SellingObjectives>> GetAllAcceptedActif();
        Task<IEnumerable<SellingObjectives>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SellingObjectives>> GetAllPending();
        Task<IEnumerable<SellingObjectives>> GetAllRejected();
        Task<SellingObjectives> GetByIdActif(int id);

        Task<IEnumerable<SellingObjectives>> GetByIdSellingObjectives(int id);

    }
}
