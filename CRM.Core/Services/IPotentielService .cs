using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPotentielService
    {
        Task<Potentiel> GetById(int id);
        Task<Potentiel> Create(Potentiel newPotentiel);
        Task<List<Potentiel>> CreateRange(List<Potentiel> newPotentiel);
        Task Update(Potentiel PotentielToBeUpdated, Potentiel Potentiel);
        Task Delete(Potentiel PotentielToBeDeleted);
        Task DeleteRange(List<Potentiel> Potentiel);

        Task<IEnumerable<Potentiel>> GetAll();
        Task<IEnumerable<Potentiel>> GetAllActif();
        Task<IEnumerable<Potentiel>> GetAllInActif();

    }
}
