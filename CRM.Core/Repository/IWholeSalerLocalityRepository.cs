using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IWholeSalerLocalityRepository : IRepository<WholeSalerLocality>
    {
        Task<IEnumerable<WholeSalerLocality>> GetAllActif();
        Task<IEnumerable<WholeSalerLocality>> GetAllInActif();
        Task<IEnumerable<WholeSalerLocality>> GetAllAcceptedActif();
        Task<IEnumerable<WholeSalerLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<WholeSalerLocality>> GetAllPending();
        Task<IEnumerable<WholeSalerLocality>> GetAllRejected();
        Task<WholeSalerLocality> GetByIdActif(int id);

    }
}
