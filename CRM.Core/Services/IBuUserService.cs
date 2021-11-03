using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IBuUserService
    {
        Task<BuUser> GetById(int id);
        Task<BuUser> Create(BuUser newBuUser);
        Task<List<BuUser>> CreateRange(List<BuUser> newBuUser);
        Task Delete(BuUser BuUserToBeDeleted);
        Task DeleteRange(List<BuUser> BuUser);

        Task<IEnumerable<BuUser>> GetAll();
        Task<IEnumerable<BuUser>> GetAllActif();
        Task<IEnumerable<BuUser>> GetAllInActif();

    }
}
