using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllActif();
        Task<IEnumerable<User>> GetAllInActif();
        Task<IEnumerable<User>> GetAllAcceptedActif();
        Task<IEnumerable<User>> GetAllAcceptedInactifActif();
        Task<IEnumerable<User>> GetAllPending();
        Task<IEnumerable<User>> GetAllRejected();
        Task<User> GetByIdActif(int id);

    }
}
