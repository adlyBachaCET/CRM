using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PharmacyService : IPharmacyService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PharmacyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pharmacy> Create(Pharmacy newPharmacy)
        {

            await _unitOfWork.Pharmacys.Add(newPharmacy);
            await _unitOfWork.CommitAsync();
            return newPharmacy;
        }
        public async Task<List<Pharmacy>> CreateRange(List<Pharmacy> newPharmacy)
        {

            await _unitOfWork.Pharmacys.AddRange(newPharmacy);
            await _unitOfWork.CommitAsync();
            return newPharmacy;
        }
        public async Task<IEnumerable<Pharmacy>> GetPharmacysAssigned()
        {
        
            return await _unitOfWork.Pharmacys.GetPharmacysAssigned();
        }
        public async Task<IEnumerable<Pharmacy>> GetAll()
        {
            return
                           await _unitOfWork.Pharmacys.GetAll();
        }
        public async Task<PharmacyExiste> Verify(SaveAddPharmacyResource SaveAddPharmacyResource)
        {
            PharmacyExiste Object = new PharmacyExiste();
            Object.ExistPharmacyEmail = false;
            Object.ExistPharmacyFirstName = false;
            Object.ExistPharmacyLastName= false;
            Object.ExistPharmacyName= false;


            var PharmacyEmail = await _unitOfWork.Pharmacys.GetByExistantEmailActif(SaveAddPharmacyResource.SavePharmacyResource.Email);
            var PharmacyFirstNameOwner = await _unitOfWork.Pharmacys.GetByExistantFirstNameActif(SaveAddPharmacyResource.SavePharmacyResource.FirstNameOwner);
            var PharmacyLastNameOwner = await _unitOfWork.Pharmacys.GetByExistantLastNameActif( SaveAddPharmacyResource.SavePharmacyResource.LastNameOwner);
            var PharmacyName = await _unitOfWork.Pharmacys.GetByExistantNameActif(SaveAddPharmacyResource.SavePharmacyResource.Name);


            if (PharmacyEmail != null)
            {
                Object.PharmacyEmail = PharmacyEmail;
                Object.ExistPharmacyEmail = true;
            }
            if (PharmacyFirstNameOwner != null)
            {
                Object.PharmacyFirstName = PharmacyFirstNameOwner;
                Object.ExistPharmacyFirstName = true;
            }
            if (PharmacyLastNameOwner!= null)
            {
                Object.PharmacyLastName = PharmacyEmail;
                Object.ExistPharmacyLastName = true;
            }
            if (PharmacyName != null)
            {
                Object.PharmacyName = PharmacyName;
                Object.ExistPharmacyName = true;
            }
            return
                           Object;
        }
        

        public async Task<Pharmacy> GetById(int? id)
        {
            return
                      await _unitOfWork.Pharmacys.GetByIdActif(id);
        }
   
        public async Task Update(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy)
        {
            PharmacyToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Pharmacy.Version = PharmacyToBeUpdated.Version + 1;
            Pharmacy.Id = PharmacyToBeUpdated.Id;
            Pharmacy.Status = Status.Pending;
            Pharmacy.Active = 0;

            await _unitOfWork.Pharmacys.Add(Pharmacy);
            await _unitOfWork.CommitAsync();
        }
        public async Task Approuve(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy)
        {
            PharmacyToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Pharmacy = PharmacyToBeUpdated;
            Pharmacy.Version = PharmacyToBeUpdated.Version + 1;
            Pharmacy.Id = PharmacyToBeUpdated.Id;
            Pharmacy.Status = Status.Rejected;
            Pharmacy.UpdatedOn = System.DateTime.UtcNow;
            Pharmacy.CreatedOn = PharmacyToBeUpdated.CreatedOn;

            Pharmacy.Active = 0;

            await _unitOfWork.Pharmacys.Add(Pharmacy);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy)
        {
            PharmacyToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Pharmacy.Version = PharmacyToBeUpdated.Version + 1;
            Pharmacy.Id = PharmacyToBeUpdated.Id;
            Pharmacy.Status = Status.Rejected;
            Pharmacy.Active = 1;

            await _unitOfWork.Pharmacys.Add(Pharmacy);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Pharmacy Pharmacy)
        {
            Pharmacy.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Pharmacy> Pharmacy)
        {
            foreach (var item in Pharmacy)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Pharmacy>> GetAllActif()
        {
            return
                             await _unitOfWork.Pharmacys.GetAllActif();
        }

        public async Task<IEnumerable<Pharmacy>> GetAllInActif()
        {
            return
                             await _unitOfWork.Pharmacys.GetAllInActif();
        }

        public async Task<IEnumerable<Pharmacy>> GetByExistantPhoneNumberActif(int PhoneNumber)
        {
            return
                                 await _unitOfWork.Pharmacys.GetByExistantPhoneNumberActif(PhoneNumber); 
        }

        public async Task<IEnumerable<Pharmacy>> GetByNearByActif(string Locality1, string Locality2, int CodePostal)
        {
            return
                       await _unitOfWork.Pharmacys.GetByNearByActif( Locality1,  Locality2,  CodePostal);
        }

        public Task<IEnumerable<Pharmacy>> GetPharmacysNotAssignedByBu(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pharmacy>> GetMyPharmacysWithoutAppointment(int Id)
        {
            return
                   await _unitOfWork.Pharmacys.GetMyPharmacysWithoutAppointment(Id);
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmacysByLocalities(List<int> IdLocalities)
        {
            return
                await _unitOfWork.Pharmacys.GetPharmacysByLocalities(IdLocalities);
        }

        public async Task<IEnumerable<Pharmacy>> GetAll(Status? Status, GrossistePharmacy GrossistePharmacy)
        {
            return
                                         await _unitOfWork.Pharmacys.GetAll(Status,GrossistePharmacy);
        }

        public async Task<Pharmacy> GetById(int Id, Status? Status, GrossistePharmacy GrossistePharmacy)
        {
            return
                                      await _unitOfWork.Pharmacys.GetById(Id,Status,GrossistePharmacy);
        }
    }
}
