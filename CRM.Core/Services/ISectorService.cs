using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISectorService
    {
        Task<Sector> GetById(int id);
        Task<Sector> Create(Sector newSector);
        Task<List<Sector>> CreateRange(List<Sector> newSector);
        Task Update(Sector SectorToBeUpdated, Sector Sector);
        Task Delete(Sector SectorToBeDeleted);
        Task DeleteRange(List<Sector> Sector);

        Task<IEnumerable<Sector>> GetAll();
        Task<IEnumerable<Sector>> GetAllActif();
        Task<IEnumerable<Sector>> GetAllInActif();

    }
}
