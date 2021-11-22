using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleUserService : ICycleUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CycleUser> Create(CycleUser newCycleUser)
        {

            await _unitOfWork.CycleUsers.Add(newCycleUser);
            await _unitOfWork.CommitAsync();
            return newCycleUser;
        }
        public async Task<List<CycleUser>> CreateRange(List<CycleUser> newCycleUser)
        {

            await _unitOfWork.CycleUsers.AddRange(newCycleUser);
            await _unitOfWork.CommitAsync();
            return newCycleUser;
        }
        public async Task<IEnumerable<CycleUser>> GetAll()
        {
            return
                           await _unitOfWork.CycleUsers.GetAll();
        }

       /* public async Task Delete(CycleUser CycleUser)
        {
            _unitOfWork.CycleUsers.Remove(CycleUser);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<CycleUser>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.CycleUsers
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<CycleUser> GetById(int id)
        {
            return
                await _unitOfWork.CycleUsers.GetById(id);
        }
        public async Task<List<Cycle>> GetByIdUser(int id)
        {
            List<Cycle> List = new List<Cycle>();
            var CycleUser = await _unitOfWork.CycleUsers.Find(i => i.IdUser == id && i.Active == 0);
            foreach(var item in CycleUser)
            {
                var Cycle = await _unitOfWork.Cycles.SingleOrDefault(i => i.IdCycle == item.IdCycle && i.Active == 0);
                List.Add(Cycle);
            }
            return
                List;
        }

        public async Task Update(CycleUser CycleUserToBeUpdated, CycleUser CycleUser)
        {
            CycleUser.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(CycleUser CycleUser)
        {
            //CycleUser musi =  _unitOfWork.CycleUsers.SingleOrDefaultAsync(x=>x.Id == CycleUserToBeUpdated.Id);
            CycleUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<CycleUser> CycleUser)
        {
            foreach (var item in CycleUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CycleUser>> GetAllActif()
        {
            return
                             await _unitOfWork.CycleUsers.GetAllActif();
        }

        public async Task<IEnumerable<CycleUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.CycleUsers.GetAllInActif();
        }
        //public Task<CycleUser> CreateCycleUser(CycleUser newCycleUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCycleUser(CycleUser CycleUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<CycleUser> GetCycleUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<CycleUser>> GetCycleUsersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCycleUser(CycleUser CycleUserToBeUpdated, CycleUser CycleUser)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
