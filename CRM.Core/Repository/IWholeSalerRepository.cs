using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IWholeSalerRepository : IRepository<WholeSaler>
    {
        Task<IEnumerable<WholeSaler>> GetAllActif();
        Task<IEnumerable<WholeSaler>> GetAllInActif();
        Task<IEnumerable<WholeSaler>> GetAllAcceptedActif();
        Task<IEnumerable<WholeSaler>> GetAllAcceptedInactifActif();
        Task<IEnumerable<WholeSaler>> GetAllPending();
        Task<IEnumerable<WholeSaler>> GetAllRejected();
        Task<WholeSaler> GetByIdActif(int id);

    }
}
