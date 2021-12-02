using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ObjectionService : IObjectionService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ObjectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Objection> Create(Objection newObjection)
        {

            await _unitOfWork.Objections.Add(newObjection);
            await _unitOfWork.CommitAsync();
            return newObjection;
        }
        public async Task<List<Objection>> CreateRange(List<Objection> newObjection)
        {

            await _unitOfWork.Objections.AddRange(newObjection);
            await _unitOfWork.CommitAsync();
            return newObjection;
        }
        public async Task<IEnumerable<Objection>> GetAll()
        {
            return
                           await _unitOfWork.Objections.GetAll();
        }

 
        public async Task<IEnumerable<Objection>> GetAll(Status? Status, RequestObjection RequestObjection)
        {

            if (Status != null) return await _unitOfWork.Objections.Find(i => i.Status == Status && i.Active == 0 
            && i.RequestObjection==RequestObjection);

            return await _unitOfWork.Objections.Find(i => i.Active == 0
            && i.RequestObjection == RequestObjection);


        }
        public async Task<IEnumerable<Objection>> GetAll(Status? Status)
        {

            if (Status != null) return await _unitOfWork.Objections.Find(i => i.Status == Status && i.Active == 0);

            return await _unitOfWork.Objections.Find(i => i.Status == Status && i.Active == 0);


        }
        public async Task<Objection> GetById(int Id,Status? Status, RequestObjection RequestObjection)
        {

            if (Status != null) return await _unitOfWork.Objections.SingleOrDefault(i => i.IdObjection== Id&& i.Status == Status && i.Active == 0
            && i.RequestObjection == RequestObjection);

            return await _unitOfWork.Objections.SingleOrDefault(i => i.IdObjection == Id && i.Active == 0
            && i.RequestObjection == RequestObjection);


        }
        public async Task Approuve(Objection ObjectionToBeUpdated, Objection Objection)
        {
            ObjectionToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Objection = ObjectionToBeUpdated;
            Objection.Version = ObjectionToBeUpdated.Version + 1;
            Objection.IdObjection = ObjectionToBeUpdated.IdObjection;
            Objection.Status = Status.Approuved;
            Objection.UpdatedOn = System.DateTime.UtcNow;
            Objection.CreatedOn = ObjectionToBeUpdated.CreatedOn;
            Objection.Active = 0;
            await _unitOfWork.Objections.Add(Objection);
            await _unitOfWork.CommitAsync();
        }
        public async Task Reject(Objection ObjectionToBeUpdated, Objection Objection)
        {
            ObjectionToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Objection = ObjectionToBeUpdated;
            Objection.Version = ObjectionToBeUpdated.Version + 1;
            Objection.IdObjection = ObjectionToBeUpdated.IdObjection;
            Objection.Status = Status.Rejected;
            Objection.UpdatedOn = System.DateTime.UtcNow;
            Objection.CreatedOn = ObjectionToBeUpdated.CreatedOn;

            Objection.Active = 0;

            await _unitOfWork.Objections.Add(Objection);
            await _unitOfWork.CommitAsync();
        }
        /* public async Task Delete(Objection Objection)
         {
             _unitOfWork.Objections.Remove(Objection);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Objection>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Objections
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Objection> GetById(int id)
        {
             return
               await _unitOfWork.Objections.SingleOrDefault(i => i.IdObjection == id && i.Active == 0);
        }
   
        public async Task Update(Objection ObjectionToBeUpdated, Objection Objection)
        {
            ObjectionToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Objection.Version = ObjectionToBeUpdated.Version + 1;
            Objection.IdObjection = ObjectionToBeUpdated.IdObjection;
            Objection.Status = Status.Pending;
            Objection.Active = 0;

            await _unitOfWork.Objections.Add(Objection);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Objection Objection)
        {
            //Objection musi =  _unitOfWork.Objections.SingleOrDefaultAsync(x=>x.Id == ObjectionToBeUpdated.Id);
            Objection.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Objection> Objection)
        {
            foreach (var item in Objection)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Objection>> GetAllActif()
        {
            return
                             await _unitOfWork.Objections.GetAllActif();
        }

        public async Task<IEnumerable<Objection>> GetAllInActif()
        {
            return
                             await _unitOfWork.Objections.GetAllInActif();
        }

        public async Task<IEnumerable<Objection>> GetByIdDoctor(RequestObjection RequestObjection, int id)
        {
            return
                            await _unitOfWork.Objections.Find(i=>i.IdDoctor==id && i.RequestObjection== RequestObjection && i.Active==0);
        }
        public async Task<IEnumerable<Objection>> GetByIdPharmacy(RequestObjection RequestObjection, int id)
        {
            return
                            await _unitOfWork.Objections.Find(i => i.IdPharmacy == id && i.RequestObjection == RequestObjection && i.Active == 0);
        }
        public async Task<IEnumerable<Objection>> GetByIdActifDoctor(int Id)
        {
            return
                  await _unitOfWork.Objections.GetByIdActifDoctor(Id);
        }
        public async Task<IEnumerable<Objection>> GetByIdActifUser(int Id)
        {
            return
                  await _unitOfWork.Objections.GetByIdActifUser(Id);
        }
        //public Task<Objection> CreateObjection(Objection newObjection)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteObjection(Objection Objection)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Objection> GetObjectionById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Objection>> GetObjectionsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateObjection(Objection ObjectionToBeUpdated, Objection Objection)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
