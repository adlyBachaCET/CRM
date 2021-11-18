
using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IVisitUserRepository : IRepository<VisitUser>
    {
        Task<IEnumerable<VisitUser>> GetAllActif();
        Task<IEnumerable<VisitUser>> GetAllInActif();
        Task<IEnumerable<VisitUser>> GetAllAcceptedActif();
        Task<IEnumerable<VisitUser>> GetAllAcceptedInactifActif();
        Task<IEnumerable<VisitUser>> GetAllPending();
        Task<IEnumerable<VisitUser>> GetAllRejected();
        Task<VisitUser> GetByIdActif(int id);
        Task<IEnumerable<VisitUser>> GetAllById(int Id);

    }
}
