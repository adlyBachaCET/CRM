using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IExternalsRepository : IRepository<Externals>
    {
        Task<IEnumerable<Externals>> GetAllActif();
        Task<IEnumerable<Externals>> GetAllInActif();
        Task<IEnumerable<Externals>> GetAllAcceptedActif();
        Task<IEnumerable<Externals>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Externals>> GetAllPending();
        Task<IEnumerable<Externals>> GetAllRejected();
        Task<Externals> GetByIdActif(int id);
        Task<IEnumerable<Externals>> GetAllById(int Id);
    
    }
}
