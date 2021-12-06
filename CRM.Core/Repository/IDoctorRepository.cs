using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Locality>> GetLocalitiesFromDoctors(List<Doctor> DoctorList);
        Task<IEnumerable<Doctor>> GetDoctorsByLocalities(List<int> IdLocalities);
        Task<DoctorExiste> GetExist(string FirstName, string LastName, string Email);
        Task<IEnumerable<Doctor>> GetDoctorsNotAssigned();
        Task<IEnumerable<Doctor>> GetDoctorsAssigned();
        Task<IEnumerable<Doctor>> GetMyDoctorsWithoutAppointment(int Id);
        Task<IEnumerable<Doctor>> GetDoctorsNotAssignedByBu(int Id);
        Task<IEnumerable<Doctor>> GetDoctorsAssignedByBu(int Id);
        Task<IEnumerable<Doctor>> GetAllDoctorsByBu(int Id);
        Task<IEnumerable<Doctor>> GetAllActif();
        Task<IEnumerable<Doctor>> GetAllInActif();
        Task<Doctor> GetByIdActif(int id);
        Task<Doctor> GetById(int? id);

        Task<IEnumerable<Doctor>> GetAllAcceptedActif();
        Task<IEnumerable<Doctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Doctor>> GetAllPending();
        Task<IEnumerable<Doctor>> GetAllRejected();
        Task<IEnumerable<Doctor>> GetByExistantPhoneNumberActif(int PhoneNumber);
        Task<IEnumerable<Service>> GetServiceByIdLocationActif(int IdLocation);
        Task<List<Specialty>> GetByIdDoctor(int idDoctor);

    }
}
