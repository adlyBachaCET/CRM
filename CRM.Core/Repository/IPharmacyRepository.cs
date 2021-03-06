using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPharmacyRepository : IRepository<Pharmacy>
    {
        Task<IEnumerable<Pharmacy>> GetAll(Status? Status, GrossistePharmacy GrossistePharmacy);
        Task<Pharmacy> GetById(int Id, Status? Status, GrossistePharmacy GrossistePharmacy);
        Task<IEnumerable<Pharmacy>> GetPharmacysByLocalities(List<int> IdLocalities);
        Task<IEnumerable<Pharmacy>> GetMyPharmacysWithoutAppointment(int Id);
        Task<IEnumerable<Pharmacy>> GetAllActif();
        Task<IEnumerable<Pharmacy>> GetAllInActif();
        Task<IEnumerable<Pharmacy>> GetAllAcceptedActif();
        Task<IEnumerable<Pharmacy>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Pharmacy>> GetAllPending();
        Task<IEnumerable<Pharmacy>> GetAllRejected();
        Task<Pharmacy> GetByIdActif(int? id);
        Task<Pharmacy> GetByExistantEmailActif(string Email);
        Task<Pharmacy> GetByExistantLastNameActif( string LastName);
        Task<IEnumerable<Pharmacy>> GetPharmacysAssigned();

        Task<Pharmacy> GetByExistantNameActif(string Name);

        Task<Pharmacy> GetByExistantFirstNameActif(string FirstName);
        Task<IEnumerable<Pharmacy>> GetByExistantPhoneNumberActif(int PhoneNumber);
        Task<IEnumerable<Pharmacy>> GetByNearByActif(string Locality1, string Locality2, int CodePostal);
     
    }
}
