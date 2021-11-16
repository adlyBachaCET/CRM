using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IVisitReportRepository : IRepository<VisitReport>
    {
        Task<IEnumerable<VisitReport>> GetAllActif();
        Task<IEnumerable<VisitReport>> GetAllInActif();
        Task<IEnumerable<VisitReport>> GetAllAcceptedActif();
        Task<IEnumerable<VisitReport>> GetAllAcceptedInactifActif();
        Task<IEnumerable<VisitReport>> GetAllPending();
        Task<IEnumerable<VisitReport>> GetAllRejected();
        Task<VisitReport> GetByIdActif(int id);
        Task<IEnumerable<VisitReport>> GetAllById(int Id);
        Task<VisitReport> GetByIdDoctor(int id);
    }
}
