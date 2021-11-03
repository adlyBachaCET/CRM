using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleBuService : ICycleBuService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleBuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CycleBu> Create(CycleBu newCycleBu)
        {

            await _unitOfWork.CycleBus.Add(newCycleBu);
            await _unitOfWork.CommitAsync();
            return newCycleBu;
        }
        public async Task<List<CycleBu>> CreateRange(List<CycleBu> newCycleBu)
        {

            await _unitOfWork.CycleBus.AddRange(newCycleBu);
            await _unitOfWork.CommitAsync();
            return newCycleBu;
        }
        public async Task<IEnumerable<CycleBu>> GetAll()
        {
            return
                           await _unitOfWork.CycleBus.GetAll();
        }

       /* public async Task Delete(CycleBu CycleBu)
        {
            _unitOfWork.CycleBus.Remove(CycleBu);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<CycleBu>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.CycleBus
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<CycleBu> GetById(int id)
        {
            return
                await _unitOfWork.CycleBus.GetById(id);
        }
   
        public async Task Update(CycleBu CycleBuToBeUpdated, CycleBu CycleBu)
        {
            CycleBu.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(CycleBu CycleBu)
        {
            //CycleBu musi =  _unitOfWork.CycleBus.SingleOrDefaultAsync(x=>x.Id == CycleBuToBeUpdated.Id);
            CycleBu.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<CycleBu> CycleBu)
        {
            foreach (var item in CycleBu)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CycleBu>> GetAllActif()
        {
            return
                             await _unitOfWork.CycleBus.GetAllActif();
        }

        public async Task<IEnumerable<CycleBu>> GetAllInActif()
        {
            return
                             await _unitOfWork.CycleBus.GetAllInActif();
        }
        //public Task<CycleBu> CreateCycleBu(CycleBu newCycleBu)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCycleBu(CycleBu CycleBu)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<CycleBu> GetCycleBuById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<CycleBu>> GetCycleBusByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCycleBu(CycleBu CycleBuToBeUpdated, CycleBu CycleBu)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
