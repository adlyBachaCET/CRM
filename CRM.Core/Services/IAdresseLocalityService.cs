using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IAdresseLocalityService
    {
        Task<AdresseLocality> GetById(int id);
        Task<AdresseLocality> Create(AdresseLocality newAdresseLocality);
        Task<List<AdresseLocality>> CreateRange(List<AdresseLocality> newAdresseLocality);
        Task Delete(AdresseLocality AdresseLocalityToBeDeleted);
        Task DeleteRange(List<AdresseLocality> AdresseLocality);

        Task<IEnumerable<AdresseLocality>> GetAll();
        Task<IEnumerable<AdresseLocality>> GetAllActif();
        Task<IEnumerable<AdresseLocality>> GetAllInActif();

    }
}
