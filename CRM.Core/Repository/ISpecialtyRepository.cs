using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ISpecialtyRepository : IRepository<Specialty>
    {
        Task<IEnumerable<Specialty>> GetAllActif();
        Task<IEnumerable<Specialty>> GetAllInActif();
        Task<IEnumerable<Specialty>> GetAllAcceptedActif();
        Task<IEnumerable<Specialty>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Specialty>> GetAllPending();
        Task<IEnumerable<Specialty>> GetAllRejected();
        Task<Specialty> GetByIdActif(int id);

    }
}
