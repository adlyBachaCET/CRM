using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPotentielCycleService
    {
        Task<PotentielCycle> GetById(int id);
        Task<PotentielCycle> GetByIdPotentielCycle(int IdPotentiel, int idCycle);

        Task<PotentielCycle> Create(PotentielCycle newPotentielCycle);
        Task<List<PotentielCycle>> CreateRange(List<PotentielCycle> newPotentielCycle);
        Task Delete(PotentielCycle PotentielCycleToBeDeleted);
        Task DeleteRange(List<PotentielCycle> PotentielCycle);
        Task<IEnumerable<Potentiel>> GetPotentielsById(int id);
        Task<IEnumerable<PotentielCycle>> GetAll();
        Task<IEnumerable<PotentielCycle>> GetAllActif();
        Task<IEnumerable<PotentielCycle>> GetAllInActif();

    }
}
