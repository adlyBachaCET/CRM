using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class AppointementService : IAppointementService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public AppointementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointement> Create(Appointement newAppointement)
        {

            await _unitOfWork.Appointements.Add(newAppointement);
            await _unitOfWork.CommitAsync();
            return newAppointement;
        }
        public async Task<List<Appointement>> CreateRange(List<Appointement> newAppointement)
        {

            await _unitOfWork.Appointements.AddRange(newAppointement);
            await _unitOfWork.CommitAsync();
            return newAppointement;
        }
  
        public async Task<IEnumerable<Appointement>> GetAll()
        {
            return
                           await _unitOfWork.Appointements.GetAll();
        }
       
        /* public async Task Delete(Appointement Appointement)
         {
             _unitOfWork.Appointements.Remove(Appointement);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Appointement>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Appointements
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Appointement> GetById(int? id)
        {
            return
                      await _unitOfWork.Appointements.SingleOrDefault(i => i.IdAppointement == id && i.Active == 0);
        }
        public async Task<List<Appointement>> GetByIdUser(int id)
        {   List<Appointement> Activities = new List<Appointement>();
            var List = await _unitOfWork.Appointements.Find(i => i.IdUser == id && i.Active == 0);
            foreach(var item in List)
            {
                var Appointement = await _unitOfWork.Appointements.SingleOrDefault(i => i.IdAppointement == item.IdAppointement && i.Active == 0);

                Activities.Add(Appointement);
            }
                return
                      Activities;
        }
        public async Task Update(Appointement AppointementToBeUpdated, Appointement Appointement)
        {
            AppointementToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Appointement.Version = AppointementToBeUpdated.Version + 1;
            Appointement.IdAppointement = AppointementToBeUpdated.IdAppointement;
            Appointement.Status = Status.Pending;
            Appointement.Active = 0;

            await _unitOfWork.Appointements.Add(Appointement);
            await _unitOfWork.CommitAsync();
        }
        public async Task Approuve(Appointement AppointementToBeUpdated, Appointement Appointement)
        {
            AppointementToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Appointement = AppointementToBeUpdated;
            Appointement.Version = AppointementToBeUpdated.Version + 1;
            Appointement.IdAppointement = AppointementToBeUpdated.IdAppointement;
            Appointement.Status = Status.Rejected;
            Appointement.UpdatedOn = System.DateTime.UtcNow;
            Appointement.CreatedOn = AppointementToBeUpdated.CreatedOn;

            Appointement.Active = 0;

            await _unitOfWork.Appointements.Add(Appointement);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Appointement AppointementToBeUpdated, Appointement Appointement)
        {
            AppointementToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Appointement.Version = AppointementToBeUpdated.Version + 1;
            Appointement.IdAppointement = AppointementToBeUpdated.IdAppointement;
            Appointement.Status = Status.Rejected;
            Appointement.Active = 1;

            await _unitOfWork.Appointements.Add(Appointement);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Appointement Appointement)
        {
            //Appointement musi =  _unitOfWork.Appointements.SingleOrDefaultAsync(x=>x.Id == AppointementToBeUpdated.Id);
            Appointement.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Appointement> Appointement)
        {
            foreach (var item in Appointement)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Appointement>> GetAllActif()
        {
            return
                             await _unitOfWork.Appointements.GetAllActif();
        }

        public async Task<IEnumerable<Appointement>> GetAllInActif()
        {
            return
                             await _unitOfWork.Appointements.GetAllInActif();
        }



        public Task<IEnumerable<Appointement>> GetAppointementsNotAssignedByBu(int Id)
        {
            throw new NotImplementedException();
        }
        //public Task<Appointement> CreateAppointement(Appointement newAppointement)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteAppointement(Appointement Appointement)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Appointement> GetAppointementById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Appointement>> GetAppointementsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAppointement(Appointement AppointementToBeUpdated, Appointement Appointement)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
