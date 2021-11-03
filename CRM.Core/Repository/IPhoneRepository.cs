using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        Task<IEnumerable<Phone>> GetAllActif();
        Task<IEnumerable<Phone>> GetAllInActif();
        Task<IEnumerable<Phone>> GetAllAcceptedActif();
        Task<IEnumerable<Phone>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Phone>> GetAllPending();
        Task<IEnumerable<Phone>> GetAllRejected();
        Task<Phone> GetByIdActif(int id);

    }
}
