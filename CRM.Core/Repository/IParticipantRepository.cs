using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByIdDoctor(int id);
        Task<IEnumerable<Participant>> GetAllActif();
        Task<IEnumerable<Participant>> GetAllInActif();
        Task<IEnumerable<Participant>> GetAllAcceptedActif();
        Task<IEnumerable<Participant>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Participant>> GetAllPending();
        Task<IEnumerable<Participant>> GetAllRejected();
        Task<Participant> GetByIdActif(int id);
        Task<IEnumerable<Participant>> GetAllByIdPharmacy(int Id);
        Task<IEnumerable<Participant>> GetAllById(int Id);
        Task<IEnumerable<Participant>> GetAllByIdDoctor(int Id);
        Task<IEnumerable<Participant>> GetAllByIdRequest(int Id);
    }
}
