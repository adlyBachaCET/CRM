using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IPharmacyRepository : IRepository<Pharmacy>
    {
        Task<IEnumerable<Pharmacy>> GetAllActif();
        Task<IEnumerable<Pharmacy>> GetAllInActif();
        Task<IEnumerable<Pharmacy>> GetAllAcceptedActif();
        Task<IEnumerable<Pharmacy>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Pharmacy>> GetAllPending();
        Task<IEnumerable<Pharmacy>> GetAllRejected();
        Task<Pharmacy> GetByIdActif(int id);

    }
}
