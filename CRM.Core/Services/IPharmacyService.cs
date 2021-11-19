using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPharmacyService
    {
        Task<Pharmacy> GetById(int? id);
        Task<Pharmacy> Create(Pharmacy newPharmacy);
        Task<List<Pharmacy>> CreateRange(List<Pharmacy> newPharmacy);
        Task Update(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy);
        Task Delete(Pharmacy PharmacyToBeDeleted);
        Task DeleteRange(List<Pharmacy> Pharmacy);

        Task<IEnumerable<Pharmacy>> GetAll();
        Task<IEnumerable<Pharmacy>> GetAllActif();
        Task<IEnumerable<Pharmacy>> GetAllInActif();
        
        Task Approuve(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy);
        Task Reject(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy);
        Task<PharmacyExiste> Verify(SaveAddPharmacyResource SaveAddPharmacyResource);
        Task<IEnumerable<Pharmacy>> GetByExistantPhoneNumberActif(int PhoneNumber);
        Task<IEnumerable<Pharmacy>> GetByNearByActif(string Locality1, string Locality2, int CodePostal);
        Task<IEnumerable<Pharmacy>> GetPharmacysAssigned();
        Task<IEnumerable<Pharmacy>> GetPharmacysNotAssignedByBu(int Id);

    }
}
