using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPotentielSectorService
    {
        Task<PotentielSector> GetById(int id);
        Task<PotentielSector> GetByIdPotentielSector(int IdPotentiel, int idSector);

        Task<PotentielSector> Create(PotentielSector newPotentielSector);
        Task<List<PotentielSector>> CreateRange(List<PotentielSector> newPotentielSector);
        Task Delete(PotentielSector PotentielSectorToBeDeleted);
        Task DeleteRange(List<PotentielSector> PotentielSector);
        Task<IEnumerable<Potentiel>> GetPotentielsById(int id);
        Task<IEnumerable<PotentielSector>> GetAll();
        Task<IEnumerable<PotentielSector>> GetAllActif();
        Task<IEnumerable<PotentielSector>> GetAllInActif();

    }
}
