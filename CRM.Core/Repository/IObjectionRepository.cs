using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IObjectionRepository : IRepository<Objection>
    {
        Task<IEnumerable<Objection>> GetAllActif();
        Task<IEnumerable<Objection>> GetAllInActif();
        Task<IEnumerable<Objection>> GetAllAcceptedActif();
        Task<IEnumerable<Objection>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Objection>> GetAllPending();
        Task<IEnumerable<Objection>> GetAllRejected();
        Task<Objection> GetByIdActif(int id);
        Task<IEnumerable<Objection>> GetByIdDoctor(int id);
        Task<IEnumerable<Objection>> GetByIdActifDoctor(int Id);
    }
}
