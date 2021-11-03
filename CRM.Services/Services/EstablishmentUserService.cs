using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentUserService : IEstablishmentUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstablishmentUser> Create(EstablishmentUser newEstablishmentUser)
        {

            await _unitOfWork.EstablishmentUsers.Add(newEstablishmentUser);
            await _unitOfWork.CommitAsync();
            return newEstablishmentUser;
        }
        public async Task<List<EstablishmentUser>> CreateRange(List<EstablishmentUser> newEstablishmentUser)
        {

            await _unitOfWork.EstablishmentUsers.AddRange(newEstablishmentUser);
            await _unitOfWork.CommitAsync();
            return newEstablishmentUser;
        }
        public async Task<IEnumerable<EstablishmentUser>> GetAll()
        {
            return
                           await _unitOfWork.EstablishmentUsers.GetAll();
        }

       /* public async Task Delete(EstablishmentUser EstablishmentUser)
        {
            _unitOfWork.EstablishmentUsers.Remove(EstablishmentUser);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentUser>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentUsers
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<EstablishmentUser> GetById(int id)
        {
            return
                await _unitOfWork.EstablishmentUsers.GetById(id);
        }
   
        public async Task Update(EstablishmentUser EstablishmentUserToBeUpdated, EstablishmentUser EstablishmentUser)
        {
            EstablishmentUser.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(EstablishmentUser EstablishmentUser)
        {
            //EstablishmentUser musi =  _unitOfWork.EstablishmentUsers.SingleOrDefaultAsync(x=>x.Id == EstablishmentUserToBeUpdated.Id);
            EstablishmentUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<EstablishmentUser> EstablishmentUser)
        {
            foreach (var item in EstablishmentUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentUsers.GetAllActif();
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.EstablishmentUsers.GetAllInActif();
        }
        //public Task<EstablishmentUser> CreateEstablishmentUser(EstablishmentUser newEstablishmentUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentUser(EstablishmentUser EstablishmentUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentUser> GetEstablishmentUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentUser>> GetEstablishmentUsersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentUser(EstablishmentUser EstablishmentUserToBeUpdated, EstablishmentUser EstablishmentUser)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
