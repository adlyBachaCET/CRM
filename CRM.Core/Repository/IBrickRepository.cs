using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IBrickRepository : IRepository<Brick>
    {
        Task<IEnumerable<Brick>> GetAllActif();
        Task<IEnumerable<Brick>> GetAllInActif();
        Task<Brick> GetByIdActif(int id);
        Task<IEnumerable<Brick>> GetAllAcceptedActif();
        Task<IEnumerable<Brick>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Brick>> GetAllPending();
        Task<IEnumerable<Brick>> GetAllRejected();
    }
}
