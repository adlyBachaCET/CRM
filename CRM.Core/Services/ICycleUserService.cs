using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ICycleUserService
    {
        Task<CycleUser> GetById(int id);

        Task<CycleUser> Create(CycleUser newCycleUser);
        Task<List<CycleUser>> CreateRange(List<CycleUser> newCycleUser);
        Task Delete(CycleUser CycleUserToBeDeleted);
        Task DeleteRange(List<CycleUser> CycleUser);
        Task<List<Cycle>> GetByIdUser(int id);

        Task<IEnumerable<CycleUser>> GetAll();
        Task<IEnumerable<CycleUser>> GetAllActif();
        Task<IEnumerable<CycleUser>> GetAllInActif();

    }
}
