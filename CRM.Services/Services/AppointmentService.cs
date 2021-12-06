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
       
     

        public async Task<Appointement> GetById(int Id)
        {
            return
                      await _unitOfWork.Appointements.GetByIdActif(Id) ;
        }
        public async Task<List<Appointement>> GetByIdUser(int Id)
        {  
                return
                      await _unitOfWork.Appointements.GetByIdUser(Id);
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
       
    }
}
