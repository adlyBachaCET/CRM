using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ITagsRepository : IRepository<Tags>
    {
        Task<IEnumerable<Tags>> GetAllActif();
        Task<IEnumerable<Tags>> GetAllInActif();
        Task<IEnumerable<Tags>> GetAllAcceptedActif();
        Task<IEnumerable<Tags>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Tags>> GetAllPending();
        Task<IEnumerable<Tags>> GetAllRejected();
        Task<Tags> GetByIdActif(int id);

    }
}
