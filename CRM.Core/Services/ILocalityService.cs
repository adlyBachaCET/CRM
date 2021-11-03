using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocalityService
    {
        Task<Locality> GetById(int id);
        Task<Locality> Create(Locality newLocality);
        Task<List<Locality>> CreateRange(List<Locality> newLocality);
        Task Update(Locality LocalityToBeUpdated, Locality Locality);
        Task Delete(Locality LocalityToBeDeleted);
        Task DeleteRange(List<Locality> Locality);

        Task<IEnumerable<Locality>> GetAll();
        Task<IEnumerable<Locality>> GetAllActif();
        Task<IEnumerable<Locality>> GetAllInActif();

    }
}
