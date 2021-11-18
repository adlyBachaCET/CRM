using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IParticipantService
    {
        Task<Participant> GetById(int id);

        Task<Participant> Create(Participant newParticipant);
        Task<List<Participant>> CreateRange(List<Participant> newParticipant);
        Task Update(Participant ParticipantToBeUpdated, Participant Participant);
        Task Delete(Participant ParticipantToBeDeleted);
        Task DeleteRange(List<Participant> Participant);

        Task<IEnumerable<Participant>> GetAll();
        Task<IEnumerable<Participant>> GetAllActif();
        Task<IEnumerable<Participant>> GetAllInActif();
        Task<IEnumerable<Participant>> GetAllById(int Id);
        Task<IEnumerable<Participant>> GetAllByIdDoctor(int Id);
        Task<IEnumerable<Participant>> GetAllByIdRequest(int Id);

    }
}
