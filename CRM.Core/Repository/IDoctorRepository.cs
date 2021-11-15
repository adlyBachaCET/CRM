using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllActif();
        Task<IEnumerable<Doctor>> GetAllInActif();
        Task<Doctor> GetByIdActif(int id);
        Task<IEnumerable<Doctor>> GetAllAcceptedActif();
        Task<IEnumerable<Doctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Doctor>> GetAllPending();
        Task<IEnumerable<Doctor>> GetAllRejected();
        Task<IEnumerable<Doctor>> GetByExistantPhoneNumberActif(int PhoneNumber);

    }
}
