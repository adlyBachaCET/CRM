using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IProductVisitReportRepository : IRepository<ProductVisitReport>
    {
        Task<IEnumerable<ProductVisitReport>> GetAllActif();
        Task<IEnumerable<ProductVisitReport>> GetAllInActif();
        Task<IEnumerable<ProductVisitReport>> GetAllAcceptedActif();
        Task<IEnumerable<ProductVisitReport>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ProductVisitReport>> GetAllPending();
        Task<IEnumerable<ProductVisitReport>> GetAllRejected();
        Task<ProductVisitReport> GetByIdActif(int id);

        Task<IEnumerable<ProductVisitReport>> GetByIdVisitReport(int id);

    }
}
