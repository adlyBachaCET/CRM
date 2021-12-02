using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IVisitReportService
    {
        Task<VisitReport> GetById(int? id);

        Task<VisitReport> Create(VisitReport newVisitReport);
        Task<List<VisitReport>> CreateRange(List<VisitReport> newVisitReport);
        Task Update(VisitReport VisitReportToBeUpdated, VisitReport VisitReport);
        Task Delete(VisitReport VisitReportToBeDeleted);
        Task DeleteRange(List<VisitReport> VisitReport);
        Task<IEnumerable<VisitReport>> GetByIdPharmacy(int id);
        Task<IEnumerable<VisitReport>> GetAll();
        Task<IEnumerable<VisitReport>> GetAllActif();
        Task<IEnumerable<VisitReport>> GetAllInActif();
        Task<IEnumerable<VisitReport>> GetAllById(int Id);
        Task<IEnumerable<VisitReport>> GetByIdDoctor(int id);
        Task<VisitReport> GetById(int Id, Status? Status);
        Task<IEnumerable<VisitReport>> GetAll(Status? Status);
        Task Approuve(VisitReport VisitReportToBeUpdated, VisitReport VisitReport);
        Task Reject(VisitReport VisitReportToBeUpdated, VisitReport VisitReport);

    }
}
