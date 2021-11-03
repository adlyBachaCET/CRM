using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ICycleSectorWeekDoctorsRepository : IRepository<CycleSectorWeekDoctors>
    {
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllActif();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllInActif();
        Task<CycleSectorWeekDoctors> GetByIdActif(int id);
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllAcceptedActif();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllAcceptedInactifActif();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllPending();
        Task<IEnumerable<CycleSectorWeekDoctors>> GetAllRejected();
    }
}
