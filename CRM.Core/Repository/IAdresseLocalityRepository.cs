using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IAdresseLocalityRepository : IRepository<AdresseLocality>
    {
        Task<IEnumerable<AdresseLocality>> GetAllActif();
        Task<IEnumerable<AdresseLocality>> GetAllInActif();
        Task<IEnumerable<AdresseLocality>> GetAllAcceptedActif();
        Task<IEnumerable<AdresseLocality>> GetAllAcceptedInactifActif();
        Task<IEnumerable<AdresseLocality>> GetAllPending();
        Task<IEnumerable<AdresseLocality>> GetAllRejected();
        Task<AdresseLocality> GetByIdActif(int id);

    }
}
