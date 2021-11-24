using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPlanificationRepository : IRepository<Planification>
    {
        Task<IEnumerable<Planification>> GetAllActif();
        Task<IEnumerable<Planification>> GetAllInActif();
        Task<IEnumerable<Planification>> GetAllAcceptedActif();
        Task<IEnumerable<Planification>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Planification>> GetAllPending();
        Task<IEnumerable<Planification>> GetAllRejected();
        Task<Planification> GetByIdActif(int id);
        Task<IEnumerable<Planification>> GetAllById(int Id);

    }
}
