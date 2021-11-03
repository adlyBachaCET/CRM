using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IBrickLocalityService
    {
        Task<BrickLocality> GetById(int id);
        Task<BrickLocality> Create(BrickLocality newBrickLocality);
        Task<List<BrickLocality>> CreateRange(List<BrickLocality> newBrickLocality);
        Task Delete(BrickLocality BrickLocalityToBeDeleted);
        Task DeleteRange(List<BrickLocality> BrickLocality);

        Task<IEnumerable<BrickLocality>> GetAll();
        Task<IEnumerable<BrickLocality>> GetAllActif();
        Task<IEnumerable<BrickLocality>> GetAllInActif();

    }
}
