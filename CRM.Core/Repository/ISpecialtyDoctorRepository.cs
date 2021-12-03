using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISpecialtyDoctorRepository : IRepository<SpecialtyDoctor>
    {
        Task<IEnumerable<SpecialtyDoctor>> GetAllActif();
        Task<IEnumerable<SpecialtyDoctor>> GetAllInActif();
        Task<SpecialtyDoctor> GetByIdActif(int id);
        Task<IEnumerable<SpecialtyDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<SpecialtyDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<SpecialtyDoctor>> GetAllPending();
        Task<IEnumerable<SpecialtyDoctor>> GetAllRejected();
        Task<SpecialtyDoctor> GetById(int idDoctor, int idSpecialty);
    }
}
