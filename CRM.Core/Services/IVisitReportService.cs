using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IVisitReportService
    {
        Task<VisitReport> GetById(int id);

        Task<VisitReport> Create(VisitReport newVisitReport);
        Task<List<VisitReport>> CreateRange(List<VisitReport> newVisitReport);
        Task Update(VisitReport VisitReportToBeUpdated, VisitReport VisitReport);
        Task Delete(VisitReport VisitReportToBeDeleted);
        Task DeleteRange(List<VisitReport> VisitReport);

        Task<IEnumerable<VisitReport>> GetAll();
        Task<IEnumerable<VisitReport>> GetAllActif();
        Task<IEnumerable<VisitReport>> GetAllInActif();
        Task<IEnumerable<VisitReport>> GetAllById(int Id);
        Task<VisitReport> GetByIdDoctor(int id);
    }
}
