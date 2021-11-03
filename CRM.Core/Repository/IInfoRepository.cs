using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IInfoRepository : IRepository<Info>
    {
        Task<IEnumerable<Info>> GetAllActif();
        Task<IEnumerable<Info>> GetAllInActif();
        Task<IEnumerable<Info>> GetAllAcceptedActif();
        Task<IEnumerable<Info>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Info>> GetAllPending();
        Task<IEnumerable<Info>> GetAllRejected();
        Task<Info> GetByIdActif(int id);

    }
}
