
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class VisitUserService : IVisitUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public VisitUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VisitUser> Create(VisitUser newVisitUser)
        {

            await _unitOfWork.VisitUsers.Add(newVisitUser);
            await _unitOfWork.CommitAsync();
            return newVisitUser;
        }
        public async Task<List<VisitUser>> CreateRange(List<VisitUser> newVisitUser)
        {

            await _unitOfWork.VisitUsers.AddRange(newVisitUser);
            await _unitOfWork.CommitAsync();
            return newVisitUser;
        }
        public async Task<IEnumerable<VisitUser>> GetAll()
        {
            return
                           await _unitOfWork.VisitUsers.GetAll();
        }

        /* public async Task Delete(VisitUser VisitUser)
         {
             _unitOfWork.VisitUsers.Remove(VisitUser);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<VisitUser>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.VisitUsers
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<VisitUser> GetById(int id)
        {
            return
               await _unitOfWork.VisitUsers.GetById(id);
        }

     
   

        public async Task<IEnumerable<VisitUser>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.VisitUsers.GetAllById(id);
        }
        public async Task<IEnumerable<VisitUser>> GetAllById(int id)
        {
            return
               await _unitOfWork.VisitUsers.GetAllById(id);
        }
        public async Task Update(VisitUser VisitUserToBeUpdated, VisitUser VisitUser)
        {
            VisitUserToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            VisitUser.Version = VisitUserToBeUpdated.Version + 1;
            //VisitUser.IdVisitUser = VisitUserToBeUpdated.IdVisitUser;
            VisitUser.Status = Status.Pending;
            VisitUser.Active = 0;

            await _unitOfWork.VisitUsers.Add(VisitUser);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(VisitUser VisitUser)
        {
            //VisitUser musi =  _unitOfWork.VisitUsers.SingleOrDefaultAsync(x=>x.Id == VisitUserToBeUpdated.Id);
            VisitUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<VisitUser> VisitUser)
        {
            foreach (var item in VisitUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<VisitUser>> GetAllActif()
        {
            return
                             await _unitOfWork.VisitUsers.GetAllActif();
        }

        public async Task<IEnumerable<VisitUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.VisitUsers.GetAllInActif();
        }


        //public Task<VisitUser> CreateVisitUser(VisitUser newVisitUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteVisitUser(VisitUser VisitUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<VisitUser> GetVisitUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<VisitUser>> GetVisitUsersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateVisitUser(VisitUser VisitUserToBeUpdated, VisitUser VisitUser)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
