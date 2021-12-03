using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IParticipantVisitRepository : IRepository<ParticipantVisit>
    {
        Task<IEnumerable<ParticipantVisit>> GetAllActif();
        Task<IEnumerable<ParticipantVisit>> GetAllInActif();
        Task<IEnumerable<ParticipantVisit>> GetAllAcceptedActif();
        Task<IEnumerable<ParticipantVisit>> GetAllAcceptedInactifActif();
        Task<IEnumerable<ParticipantVisit>> GetAllPending();
        Task<IEnumerable<ParticipantVisit>> GetAllRejected();
        Task<ParticipantVisit> GetByIdActif(int id);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdPharmacy(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAllById(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdDoctor(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdVisitReport(int Id);
    }
}
