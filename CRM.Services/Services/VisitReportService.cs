
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class VisitReportService : IVisitReportService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public VisitReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<VisitReport>> GetAll(Status? Status)
        {

            if (Status != null) return await _unitOfWork.VisitReports.Find(i => i.Status == Status && i.Active == 0);

            return await _unitOfWork.VisitReports.Find(i => i.Status == Status && i.Active == 0);


        }
        public async Task<VisitReport> GetById(int Id, Status? Status)
        {

            if (Status != null) return await _unitOfWork.VisitReports.SingleOrDefault(i => i.IdReport == Id && i.Status == Status 
            && i.Active == 0);

            return await _unitOfWork.VisitReports.SingleOrDefault(i => i.IdReport == Id && i.Active == 0);


        }
        public async Task Approuve(VisitReport VisitReportToBeUpdated, VisitReport VisitReport)
        {
            VisitReportToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            VisitReport = VisitReportToBeUpdated;
            VisitReport.Version = VisitReportToBeUpdated.Version + 1;
            VisitReport.IdReport = VisitReportToBeUpdated.IdReport;
            VisitReport.Status = Status.Approuved;
            VisitReport.UpdatedOn = System.DateTime.UtcNow;
            VisitReport.CreatedOn = VisitReportToBeUpdated.CreatedOn;
            VisitReport.Active = 0;
            await _unitOfWork.VisitReports.Add(VisitReport);
            await _unitOfWork.CommitAsync();
        }
        public async Task Reject(VisitReport VisitReportToBeUpdated, VisitReport VisitReport)
        {
            VisitReportToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            VisitReport = VisitReportToBeUpdated;
            VisitReport.Version = VisitReportToBeUpdated.Version + 1;
            VisitReport.IdReport = VisitReportToBeUpdated.IdReport;
            VisitReport.Status = Status.Rejected;
            VisitReport.UpdatedOn = System.DateTime.UtcNow;
            VisitReport.CreatedOn = VisitReportToBeUpdated.CreatedOn;

            VisitReport.Active = 0;

            await _unitOfWork.VisitReports.Add(VisitReport);
            await _unitOfWork.CommitAsync();
        }
        public async Task<VisitReport> Create(VisitReport newVisitReport)
        {

            await _unitOfWork.VisitReports.Add(newVisitReport);
            await _unitOfWork.CommitAsync();
            return newVisitReport;
        }
        public async Task<List<VisitReport>> CreateRange(List<VisitReport> newVisitReport)
        {

            await _unitOfWork.VisitReports.AddRange(newVisitReport);
            await _unitOfWork.CommitAsync();
            return newVisitReport;
        }
        public async Task<IEnumerable<VisitReport>> GetAll()
        {
            return
                           await _unitOfWork.VisitReports.GetAll();
        }

        /* public async Task Delete(VisitReport VisitReport)
         {
             _unitOfWork.VisitReports.Remove(VisitReport);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<VisitReport>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.VisitReports
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<VisitReport> GetById(int id)
        {
            return
               await _unitOfWork.VisitReports.SingleOrDefault(i => i.IdReport == id && i.Active == 0);
        }
        public async Task<IEnumerable<VisitReport>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.VisitReports.GetByIdDoctor(id);
        }

        public async Task<IEnumerable<VisitReport>> GetByIdPharmacy(int id)
        {
            return
               await _unitOfWork.VisitReports.GetByIdPharmacy(id);
        }


        public async Task<IEnumerable<VisitReport>> GetAllById(int id)
        {
            return
               await _unitOfWork.VisitReports.GetAllById(id);
        }
        public async Task Update(VisitReport VisitReportToBeUpdated, VisitReport VisitReport)
        {
            VisitReportToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            VisitReport.Version = VisitReportToBeUpdated.Version + 1;
            VisitReport.IdReport = VisitReportToBeUpdated.IdReport;
            VisitReport.Status = Status.Pending;
            VisitReport.Active = 0;

            await _unitOfWork.VisitReports.Add(VisitReport);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(VisitReport VisitReport)
        {
            //VisitReport musi =  _unitOfWork.VisitReports.SingleOrDefaultAsync(x=>x.Id == VisitReportToBeUpdated.Id);
            VisitReport.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<VisitReport> VisitReport)
        {
            foreach (var item in VisitReport)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<VisitReport>> GetAllActif()
        {
            return
                             await _unitOfWork.VisitReports.GetAllActif();
        }

        public async Task<IEnumerable<VisitReport>> GetAllInActif()
        {
            return
                             await _unitOfWork.VisitReports.GetAllInActif();
        }


        //public Task<VisitReport> CreateVisitReport(VisitReport newVisitReport)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteVisitReport(VisitReport VisitReport)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<VisitReport> GetVisitReportById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<VisitReport>> GetVisitReportsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateVisitReport(VisitReport VisitReportToBeUpdated, VisitReport VisitReport)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
