using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISpecialityDoctorRepository : IRepository<SpecialityDoctor>
    {
        Task<IEnumerable<SpecialityDoctor>> GetAllActif();
        Task<IEnumerable<SpecialityDoctor>> GetAllInActif();
        Task<IEnumerable<SpecialityDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<SpecialityDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SpecialityDoctor>> GetAllPending();
        Task<IEnumerable<SpecialityDoctor>> GetAllRejected();
        Task<SpecialityDoctor> GetByIdActif(int id);

    }
}
