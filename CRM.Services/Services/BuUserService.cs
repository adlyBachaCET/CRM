using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class BuUserService : IBuUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BuUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BuUser> Create(BuUser newBuUser)
        {

            await _unitOfWork.BuUsers.Add(newBuUser);
            await _unitOfWork.CommitAsync();
            return newBuUser;
        }
        public async Task<List<BuUser>> CreateRange(List<BuUser> newBuUser)
        {

            await _unitOfWork.BuUsers.AddRange(newBuUser);
            await _unitOfWork.CommitAsync();
            return newBuUser;
        }
        public async Task<IEnumerable<BuUser>> GetAll()
        {
            return
                           await _unitOfWork.BuUsers.GetAll();
        }

       /* public async Task Delete(BuUser BuUser)
        {
            _unitOfWork.BuUsers.Remove(BuUser);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<BuUser>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.BuUsers
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<BuUser> GetById(int id)
        {
            return
                await _unitOfWork.BuUsers.GetById(id);
        }
        public async Task<BuUser> GetByIdUser(int id)
        {
            return
                await _unitOfWork.BuUsers.SingleOrDefault(i=>i.IdUser==id);
        }

        public async Task Update(BuUser BuUserToBeUpdated, BuUser BuUser)
        {
            BuUser.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(BuUser BuUser)
        {
            //BuUser musi =  _unitOfWork.BuUsers.SingleOrDefaultAsync(x=>x.Id == BuUserToBeUpdated.Id);
            BuUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<BuUser> BuUser)
        {
            foreach (var item in BuUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<BuUser>> GetAllActif()
        {
            return
                             await _unitOfWork.BuUsers.GetAllActif();
        }

        public async Task<IEnumerable<BuUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.BuUsers.GetAllInActif();
        }
        //public Task<BuUser> CreateBuUser(BuUser newBuUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteBuUser(BuUser BuUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<BuUser> GetBuUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<BuUser>> GetBuUsersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateBuUser(BuUser BuUserToBeUpdated, BuUser BuUser)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
