using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISectorLocalityService
    {
        Task<SectorLocality> GetById(int id);
        Task<SectorLocality> Create(SectorLocality newSectorLocality);
        Task<List<SectorLocality>> CreateRange(List<SectorLocality> newSectorLocality);
        Task Delete(SectorLocality SectorLocalityToBeDeleted);
        Task DeleteRange(List<SectorLocality> SectorLocality);

        Task<IEnumerable<SectorLocality>> GetAll();
        Task<IEnumerable<SectorLocality>> GetAllActif();
        Task<IEnumerable<SectorLocality>> GetAllInActif();

    }
}
