using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetAllActif();
        Task<IEnumerable<Visit>> GetAllInActif();
        Task<IEnumerable<Visit>> GetAllAcceptedActif();
        Task<IEnumerable<Visit>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Visit>> GetAllPending();
        Task<IEnumerable<Visit>> GetAllRejected();
        Task<Visit> GetByIdActif(int id);
        Task<IEnumerable<Visit>> GetAllById(int Id);

    }
}
