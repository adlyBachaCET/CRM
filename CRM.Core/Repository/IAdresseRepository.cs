using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IAdresseRepository : IRepository<Adresse>
    {
        Task<IEnumerable<Adresse>> GetAllActif();
        Task<IEnumerable<Adresse>> GetAllInActif();
        Task<IEnumerable<Adresse>> GetAllAcceptedActif();
        Task<IEnumerable<Adresse>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Adresse>> GetAllPending();
        Task<IEnumerable<Adresse>> GetAllRejected();
        Task<Adresse> GetByIdActif(int id);

    }

}
