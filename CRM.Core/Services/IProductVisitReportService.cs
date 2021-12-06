using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IProductVisitReportService
    {
        Task<ProductVisitReport> GetById(int id);
        Task<ProductVisitReport> Create(ProductVisitReport newProductVisitReport);
        Task<List<ProductVisitReport>> CreateRange(List<ProductVisitReport> newProductVisitReport);
        Task Update(ProductVisitReport ProductVisitReportToBeUpdated, ProductVisitReport ProductVisitReport);
        Task Delete(ProductVisitReport ProductVisitReportToBeDeleted);
        Task DeleteRange(List<ProductVisitReport> ProductVisitReport);
        Task<IEnumerable<ProductVisitReport>> GetByIdVisitReport(int id);
        
    
        Task<IEnumerable<ProductVisitReport>> GetAll();
        Task<IEnumerable<ProductVisitReport>> GetAllActif();
        Task<IEnumerable<ProductVisitReport>> GetAllInActif();


    }
}
