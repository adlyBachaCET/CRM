using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class DelegateManagerService : IDelegateManagerService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public DelegateManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DelegateManager> Create(DelegateManager newDelegateManager)
        {

            await _unitOfWork.DelegateManagers.Add(newDelegateManager);
            await _unitOfWork.CommitAsync();
            return newDelegateManager;
        }
        public async Task<List<DelegateManager>> CreateRange(List<DelegateManager> newDelegateManager)
        {

            await _unitOfWork.DelegateManagers.AddRange(newDelegateManager);
            await _unitOfWork.CommitAsync();
            return newDelegateManager;
        }
        public async Task<IEnumerable<DelegateManager>> GetAll()
        {
            return
                           await _unitOfWork.DelegateManagers.GetAll();
        }

       /* public async Task Delete(DelegateManager DelegateManager)
        {
            _unitOfWork.DelegateManagers.Remove(DelegateManager);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<DelegateManager>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.DelegateManagers
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<DelegateManager> GetById(int id)
        {
            return
                await _unitOfWork.DelegateManagers.GetById(id);
        }
   
        public async Task Update(DelegateManager DelegateManagerToBeUpdated, DelegateManager DelegateManager)
        {
            DelegateManager.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(DelegateManager DelegateManager)
        {
            //DelegateManager musi =  _unitOfWork.DelegateManagers.SingleOrDefaultAsync(x=>x.Id == DelegateManagerToBeUpdated.Id);
            DelegateManager.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<DelegateManager> DelegateManager)
        {
            foreach (var item in DelegateManager)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<DelegateManager>> GetAllActif()
        {
            return
                             await _unitOfWork.DelegateManagers.GetAllActif();
        }

        public async Task<IEnumerable<DelegateManager>> GetAllInActif()
        {
            return
                             await _unitOfWork.DelegateManagers.GetAllInActif();
        }
        //public Task<DelegateManager> CreateDelegateManager(DelegateManager newDelegateManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteDelegateManager(DelegateManager DelegateManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<DelegateManager> GetDelegateManagerById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<DelegateManager>> GetDelegateManagersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateDelegateManager(DelegateManager DelegateManagerToBeUpdated, DelegateManager DelegateManager)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
