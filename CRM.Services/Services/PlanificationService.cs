using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PlanificationService : IPlanificationService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PlanificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Planification> Create(Planification newPlanification)
        {

            await _unitOfWork.Planifications.Add(newPlanification);
            await _unitOfWork.CommitAsync();
            return newPlanification;
        }
        public async Task<List<Planification>> CreateRange(List<Planification> newPlanification)
        {

            await _unitOfWork.Planifications.AddRange(newPlanification);
            await _unitOfWork.CommitAsync();
            return newPlanification;
        }
  
        public async Task<IEnumerable<Planification>> GetAll()
        {
            return
                           await _unitOfWork.Planifications.GetAll();
        }
       
        /* public async Task Delete(Planification Planification)
         {
             _unitOfWork.Planifications.Remove(Planification);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Planification>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Planifications
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Planification> GetById(int? id)
        {
            return
                      await _unitOfWork.Planifications.SingleOrDefault(i => i.IdPlanification == id && i.Active == 0);
        }
   
        public async Task Update(Planification PlanificationToBeUpdated, Planification Planification)
        {
            PlanificationToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Planification.Version = PlanificationToBeUpdated.Version + 1;
            Planification.IdPlanification = PlanificationToBeUpdated.IdPlanification;
            Planification.Status = Status.Pending;
            Planification.Active = 0;

            await _unitOfWork.Planifications.Add(Planification);
            await _unitOfWork.CommitAsync();
        }
        public async Task Approuve(Planification PlanificationToBeUpdated, Planification Planification)
        {
            PlanificationToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Planification = PlanificationToBeUpdated;
            Planification.Version = PlanificationToBeUpdated.Version + 1;
            Planification.IdPlanification = PlanificationToBeUpdated.IdPlanification;
            Planification.Status = Status.Rejected;
            Planification.UpdatedOn = System.DateTime.UtcNow;
            Planification.CreatedOn = PlanificationToBeUpdated.CreatedOn;

            Planification.Active = 0;

            await _unitOfWork.Planifications.Add(Planification);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Planification PlanificationToBeUpdated, Planification Planification)
        {
            PlanificationToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Planification.Version = PlanificationToBeUpdated.Version + 1;
            Planification.IdPlanification = PlanificationToBeUpdated.IdPlanification;
            Planification.Status = Status.Rejected;
            Planification.Active = 1;

            await _unitOfWork.Planifications.Add(Planification);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Planification Planification)
        {
            //Planification musi =  _unitOfWork.Planifications.SingleOrDefaultAsync(x=>x.Id == PlanificationToBeUpdated.Id);
            Planification.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Planification> Planification)
        {
            foreach (var item in Planification)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Planification>> GetAllActif()
        {
            return
                             await _unitOfWork.Planifications.GetAllActif();
        }

        public async Task<IEnumerable<Planification>> GetAllInActif()
        {
            return
                             await _unitOfWork.Planifications.GetAllInActif();
        }



        public Task<IEnumerable<Planification>> GetPlanificationsNotAssignedByBu(int Id)
        {
            throw new NotImplementedException();
        }
        //public Task<Planification> CreatePlanification(Planification newPlanification)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePlanification(Planification Planification)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Planification> GetPlanificationById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Planification>> GetPlanificationsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePlanification(Planification PlanificationToBeUpdated, Planification Planification)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
