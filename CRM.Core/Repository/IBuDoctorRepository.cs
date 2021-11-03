using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IBuDoctorRepository : IRepository<BuDoctor>
    {
        Task<IEnumerable<BuDoctor>> GetAllActif();
        Task<IEnumerable<BuDoctor>> GetAllInActif();
        Task<BuDoctor> GetByIdActif(int id);
        Task<IEnumerable<BuDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<BuDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<BuDoctor>> GetAllPending();
        Task<IEnumerable<BuDoctor>> GetAllRejected();

    }
}
