
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ParticipantVisitService : IParticipantVisitService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ParticipantVisitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ParticipantVisit> Create(ParticipantVisit newParticipantVisit)
        {

            await _unitOfWork.ParticipantVisits.Add(newParticipantVisit);
            await _unitOfWork.CommitAsync();
            return newParticipantVisit;
        }
        public async Task<List<ParticipantVisit>> CreateRange(List<ParticipantVisit> newParticipantVisit)
        {

            await _unitOfWork.ParticipantVisits.AddRange(newParticipantVisit);
            await _unitOfWork.CommitAsync();
            return newParticipantVisit;
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAll()
        {
            return
                           await _unitOfWork.ParticipantVisits.GetAll();
        }

        /* public async Task Delete(ParticipantVisit ParticipantVisit)
         {
             _unitOfWork.ParticipantVisits.Remove(ParticipantVisit);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<ParticipantVisit>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.ParticipantVisits
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<ParticipantVisit> GetById(int id)
        {
            return
               await _unitOfWork.ParticipantVisits.GetByIdActif(id);
        }

     
  
        public async Task<IEnumerable<ParticipantVisit>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.ParticipantVisits.Find(i => i.IdDoctor == id && i.Active == 0);
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllById(int id)
        {
            return
               await _unitOfWork.ParticipantVisits.GetAllById(id);
        }
        public async Task Update(ParticipantVisit ParticipantVisitToBeUpdated, ParticipantVisit ParticipantVisit)
        {
            ParticipantVisitToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ParticipantVisit.Version = ParticipantVisitToBeUpdated.Version + 1;
          //  ParticipantVisit.IdParticipantVisit = ParticipantVisitToBeUpdated.IdParticipantVisit;
            ParticipantVisit.Status = Status.Pending;
            ParticipantVisit.Active = 0;

            await _unitOfWork.ParticipantVisits.Add(ParticipantVisit);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ParticipantVisit ParticipantVisit)
        {
            //ParticipantVisit musi =  _unitOfWork.ParticipantVisits.SingleOrDefaultAsync(x=>x.Id == ParticipantVisitToBeUpdated.Id);
            ParticipantVisit.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ParticipantVisit> ParticipantVisit)
        {
            foreach (var item in ParticipantVisit)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllActif()
        {
            return
                             await _unitOfWork.ParticipantVisits.GetAllActif();
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllInActif()
        {
            return
                             await _unitOfWork.ParticipantVisits.GetAllInActif();
        }





        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdVisitReport(int Id)
        {
            return
                                      await _unitOfWork.ParticipantVisits.GetAllByIdVisitReport(Id);
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdDoctor(int Id)
        {
            return
                                                  await _unitOfWork.ParticipantVisits.GetAllByIdDoctor(Id);
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdPharmacy(int Id)
        {
            return
                                                  await _unitOfWork.ParticipantVisits.GetAllByIdPharmacy(Id);
        }


        //public Task<ParticipantVisit> CreateParticipantVisit(ParticipantVisit newParticipantVisit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteParticipantVisit(ParticipantVisit ParticipantVisit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ParticipantVisit> GetParticipantVisitById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ParticipantVisit>> GetParticipantVisitsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateParticipantVisit(ParticipantVisit ParticipantVisitToBeUpdated, ParticipantVisit ParticipantVisit)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
