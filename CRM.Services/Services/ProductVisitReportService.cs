using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ProductVisitReportService : IProductVisitReportService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ProductVisitReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductVisitReport> Create(ProductVisitReport newProductVisitReport)
        {

            await _unitOfWork.ProductVisitReports.Add(newProductVisitReport);
            await _unitOfWork.CommitAsync();
            return newProductVisitReport;
        }
        public async Task<List<ProductVisitReport>> CreateRange(List<ProductVisitReport> newProductVisitReport)
        {

            await _unitOfWork.ProductVisitReports.AddRange(newProductVisitReport);
            await _unitOfWork.CommitAsync();
            return newProductVisitReport;
        }
        public async Task<IEnumerable<ProductVisitReport>> GetAll()
        {
            return
                           await _unitOfWork.ProductVisitReports.GetAll();
        }

  

        public async Task<ProductVisitReport> GetById(int id)
        {
             return
               await _unitOfWork.ProductVisitReports.SingleOrDefault(i => i.IdReport == id && i.Active == 0);
        }
   
        public async Task Update(ProductVisitReport ProductVisitReportToBeUpdated, ProductVisitReport ProductVisitReport)
        {
            ProductVisitReportToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ProductVisitReport.Version = ProductVisitReportToBeUpdated.Version + 1;
            ProductVisitReport.Status = Status.Pending;
            ProductVisitReport.Active = 0;

            await _unitOfWork.ProductVisitReports.Add(ProductVisitReport);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ProductVisitReport ProductVisitReport)
        {
            ProductVisitReport.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ProductVisitReport> ProductVisitReport)
        {
            foreach (var item in ProductVisitReport)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllActif()
        {
            return
                             await _unitOfWork.ProductVisitReports.GetAllActif();
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllInActif()
        {
            return
                             await _unitOfWork.ProductVisitReports.GetAllInActif();
        }

     
        public async Task<IEnumerable<ProductVisitReport>> GetByIdVisitReport(int id)
        {
            return
                            await _unitOfWork.ProductVisitReports.GetByIdVisitReport(id);
        }
        
     
    }
}
