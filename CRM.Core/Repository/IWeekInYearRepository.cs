using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IWeekInYearRepository : IRepository<WeekInYear>
    {
        Task<IEnumerable<WeekInYear>> GetAllActif();
        Task<IEnumerable<WeekInYear>> GetAllInActif();
        Task<IEnumerable<WeekInYear>> GetAllAcceptedActif();
        Task<IEnumerable<WeekInYear>> GetAllAcceptedInactifActif();
        Task<IEnumerable<WeekInYear>> GetAllPending();
        Task<IEnumerable<WeekInYear>> GetAllRejected();
        Task<WeekInYear> GetByIdActif(int id);

    }
}
