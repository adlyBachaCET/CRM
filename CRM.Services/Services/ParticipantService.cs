
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

        /* public async Task Delete(Participant Participant)
         {
             _unitOfWork.Participants.Remove(Participant);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Participant>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Participants
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<Participant> GetById(int id)
        {
            return
               await _unitOfWork.Participants.GetByIdActif(id);
        }

     
  
        public async Task<IEnumerable<Participant>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.Participants.Find(i => i.IdDoctor == id && i.Active == 0);
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
          //  Participant.IdParticipant = ParticipantToBeUpdated.IdParticipant;
            Participant.Status = Status.Pending;
            Participant.Active = 0;

            await _unitOfWork.Participants.Add(Participant);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Participant Participant)
        {
            //Participant musi =  _unitOfWork.Participants.SingleOrDefaultAsync(x=>x.Id == ParticipantToBeUpdated.Id);
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


        //public Task<Participant> CreateParticipant(Participant newParticipant)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteParticipant(Participant Participant)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Participant> GetParticipantById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Participant>> GetParticipantsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateParticipant(Participant ParticipantToBeUpdated, Participant Participant)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
