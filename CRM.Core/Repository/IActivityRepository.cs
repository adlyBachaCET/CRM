using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IAppointementRepository : IRepository<Appointement>
    {
        Task<IEnumerable<Appointement>> GetAllActif();
        Task<IEnumerable<Appointement>> GetAllInActif();
        Task<IEnumerable<Appointement>> GetAllAcceptedActif();
        Task<IEnumerable<Appointement>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Appointement>> GetAllPending();
        Task<IEnumerable<Appointement>> GetAllRejected();
        Task<Appointement> GetByIdActif(int id);
        Task<IEnumerable<Appointement>> GetAllById(int Id);

    }
}
