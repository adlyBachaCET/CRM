
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class VisitService : IVisitService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public VisitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Visit> Create(Visit newVisit)
        {

            await _unitOfWork.Visits.Add(newVisit);
            await _unitOfWork.CommitAsync();
            return newVisit;
        }
        public async Task<List<Visit>> CreateRange(List<Visit> newVisit)
        {

            await _unitOfWork.Visits.AddRange(newVisit);
            await _unitOfWork.CommitAsync();
            return newVisit;
        }
        public async Task<IEnumerable<Visit>> GetAll(Status? Status)
        {

            if (Status != null) return await _unitOfWork.Visits.Find(i => i.Status == Status && i.Active == 0);

            return await _unitOfWork.Visits.Find(i => i.Status == Status && i.Active == 0);


        }
        public async Task<Visit> GetById(int Id, Status? Status)
        {

            if (Status != null) return await _unitOfWork.Visits.SingleOrDefault(i => i.IdVisit == Id && i.Status == Status
            && i.Active == 0);

            return await _unitOfWork.Visits.SingleOrDefault(i => i.IdVisit == Id && i.Active == 0);


        }
        public async Task Approuve(Visit VisitToBeUpdated, Visit Visit)
        {
            VisitToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Visit = VisitToBeUpdated;
            Visit.Version = VisitToBeUpdated.Version + 1;
            Visit.IdVisit = VisitToBeUpdated.IdVisit;
            Visit.Status = Status.Approuved;
            Visit.UpdatedOn = System.DateTime.UtcNow;
            Visit.CreatedOn = VisitToBeUpdated.CreatedOn;
            Visit.Active = 0;
            await _unitOfWork.Visits.Add(Visit);
            await _unitOfWork.CommitAsync();
        }
        public async Task Reject(Visit VisitToBeUpdated, Visit Visit)
        {
            VisitToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Visit = VisitToBeUpdated;
            Visit.Version = VisitToBeUpdated.Version + 1;
            Visit.IdVisit = VisitToBeUpdated.IdVisit;
            Visit.Status = Status.Rejected;
            Visit.UpdatedOn = System.DateTime.UtcNow;
            Visit.CreatedOn = VisitToBeUpdated.CreatedOn;

            Visit.Active = 0;

            await _unitOfWork.Visits.Add(Visit);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Visit>> GetAll()
        {
            return
                           await _unitOfWork.Visits.GetAll();
        }

        /* public async Task Delete(Visit Visit)
         {
             _unitOfWork.Visits.Remove(Visit);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Visit>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Visits
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<Visit> GetById(int id)
        {
            return
               await _unitOfWork.Visits.SingleOrDefault(i => i.IdVisit == id && i.Active == 0);
        }

     
   

        public async Task<IEnumerable<Visit>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.Visits.Find(i => i.IdDoctor == id && i.Active == 0);
        }
        public async Task<IEnumerable<Visit>> GetAllById(int id)
        {
            return
               await _unitOfWork.Visits.GetAllById(id);
        }
        public async Task Update(Visit VisitToBeUpdated, Visit Visit)
        {
            VisitToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Visit.Version = VisitToBeUpdated.Version + 1;
            Visit.IdVisit = VisitToBeUpdated.IdVisit;
            Visit.Status = Status.Pending;
            Visit.Active = 0;

            await _unitOfWork.Visits.Add(Visit);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Visit Visit)
        {
            //Visit musi =  _unitOfWork.Visits.SingleOrDefaultAsync(x=>x.Id == VisitToBeUpdated.Id);
            Visit.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Visit> Visit)
        {
            foreach (var item in Visit)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Visit>> GetAllActif()
        {
            return
                             await _unitOfWork.Visits.GetAllActif();
        }

        public async Task<IEnumerable<Visit>> GetAllInActif()
        {
            return
                             await _unitOfWork.Visits.GetAllInActif();
        }


        //public Task<Visit> CreateVisit(Visit newVisit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteVisit(Visit Visit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Visit> GetVisitById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Visit>> GetVisitsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateVisit(Visit VisitToBeUpdated, Visit Visit)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
