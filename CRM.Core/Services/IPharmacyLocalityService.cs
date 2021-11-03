using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPharmacyLocalityService
    {
        Task<PharmacyLocality> GetById(int id);
        Task<PharmacyLocality> Create(PharmacyLocality newPharmacyLocality);
        Task<List<PharmacyLocality>> CreateRange(List<PharmacyLocality> newPharmacyLocality);
        Task Delete(PharmacyLocality PharmacyLocalityToBeDeleted);
        Task DeleteRange(List<PharmacyLocality> PharmacyLocality);

        Task<IEnumerable<PharmacyLocality>> GetAll();
        Task<IEnumerable<PharmacyLocality>> GetAllActif();
        Task<IEnumerable<PharmacyLocality>> GetAllInActif();

    }
}
