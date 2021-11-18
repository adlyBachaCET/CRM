using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ICommandeRepository : IRepository<Commande>
    {
        Task<IEnumerable<Commande>> GetAllActif();
        Task<IEnumerable<Commande>> GetAllInActif();
        Task<IEnumerable<Commande>> GetAllAcceptedActif();
        Task<IEnumerable<Commande>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Commande>> GetAllPending();
        Task<IEnumerable<Commande>> GetAllRejected();
        Task<Commande> GetByIdActif(int id);
        Task<IEnumerable<Commande>> GetByIdDoctor(int id);
        Task<IEnumerable<Commande>> GetByIdActifDoctor(int Id);

        Task<IEnumerable<Commande>> GetByIdActifUser(int Id);

    }
}
