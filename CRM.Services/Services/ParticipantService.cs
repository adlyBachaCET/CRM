
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ParticipantService : IParticipantService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ParticipantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Participant> Create(Participant newParticipant)
        {

            await _unitOfWork.Participants.Add(newParticipant);
            await _unitOfWork.CommitAsync();
            return newParticipant;
        }
        public async Task<List<Participant>> CreateRange(List<Participant> newParticipant)
        {

            await _unitOfWork.Participants.AddRange(newParticipant);
            await _unitOfWork.CommitAsync();
            return newParticipant;
        }
        public async Task<IEnumerable<Participant>> GetAll()
        {
            return
                           await _unitOfWork.Participants.GetAll();
        }

        
   
        public async Task<Participant> GetById(int id)
        {
            return
               await _unitOfWork.Participants.GetByIdActif(id);
        }

     
  
        public async Task<IEnumerable<Participant>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.Participants.GetByIdDoctor(id);
        }
        public async Task<IEnumerable<Participant>> GetAllById(int id)
        {
            return
               await _unitOfWork.Participants.GetAllById(id);
        }
        public async Task Update(Participant ParticipantToBeUpdated, Participant Participant)
        {
            ParticipantToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Participant.Version = ParticipantToBeUpdated.Version + 1;
            Participant.Status = Status.Pending;
            Participant.Active = 0;

            await _unitOfWork.Participants.Add(Participant);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Participant Participant)
        {
            Participant.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Participant> Participant)
        {
            foreach (var item in Participant)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Participant>> GetAllActif()
        {
            return
                             await _unitOfWork.Participants.GetAllActif();
        }

        public async Task<IEnumerable<Participant>> GetAllInActif()
        {
            return
                             await _unitOfWork.Participants.GetAllInActif();
        }





        public async Task<IEnumerable<Participant>> GetAllByIdRequest(int Id)
        {
            return
                                      await _unitOfWork.Participants.GetAllByIdRequest(Id);
        }

        public async Task<IEnumerable<Participant>> GetAllByIdDoctor(int Id)
        {
            return
                                                  await _unitOfWork.Participants.GetAllByIdDoctor(Id);
        }
        public async Task<IEnumerable<Participant>> GetAllByIdPharmacy(int Id)
        {
            return
                                                  await _unitOfWork.Participants.GetAllByIdPharmacy(Id);
        }


      
    }
}
