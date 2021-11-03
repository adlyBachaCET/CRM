using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IDelegateManagerService
    {
        Task<DelegateManager> GetById(int id);
        Task<DelegateManager> Create(DelegateManager newDelegateManager);
        Task<List<DelegateManager>> CreateRange(List<DelegateManager> newDelegateManager);
        Task Delete(DelegateManager DelegateManagerToBeDeleted);
        Task DeleteRange(List<DelegateManager> DelegateManager);

        Task<IEnumerable<DelegateManager>> GetAll();
        Task<IEnumerable<DelegateManager>> GetAllActif();
        Task<IEnumerable<DelegateManager>> GetAllInActif();

    }
}
