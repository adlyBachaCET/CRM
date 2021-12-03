using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IParticipantVisitService
    {
        Task<ParticipantVisit> GetById(int id);

        Task<ParticipantVisit> Create(ParticipantVisit newParticipantVisit);
        Task<List<ParticipantVisit>> CreateRange(List<ParticipantVisit> newParticipantVisit);
        Task Update(ParticipantVisit ParticipantVisitToBeUpdated, ParticipantVisit ParticipantVisit);
        Task Delete(ParticipantVisit ParticipantVisitToBeDeleted);
        Task DeleteRange(List<ParticipantVisit> ParticipantVisit);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdPharmacy(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAll();
        Task<IEnumerable<ParticipantVisit>> GetAllActif();
        Task<IEnumerable<ParticipantVisit>> GetAllInActif();
        Task<IEnumerable<ParticipantVisit>> GetAllById(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdDoctor(int Id);
        Task<IEnumerable<ParticipantVisit>> GetAllByIdVisitReport(int Id);

    }
}
