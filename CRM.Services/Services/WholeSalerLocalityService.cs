using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class WholeSalerLocalityService : IWholeSalerLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public WholeSalerLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WholeSalerLocality> Create(WholeSalerLocality newWholeSalerLocality)
        {

            await _unitOfWork.WholeSalerLocalitys.Add(newWholeSalerLocality);
            await _unitOfWork.CommitAsync();
            return newWholeSalerLocality;
        }
        public async Task<List<WholeSalerLocality>> CreateRange(List<WholeSalerLocality> newWholeSalerLocality)
        {

            await _unitOfWork.WholeSalerLocalitys.AddRange(newWholeSalerLocality);
            await _unitOfWork.CommitAsync();
            return newWholeSalerLocality;
        }
        public async Task<IEnumerable<WholeSalerLocality>> GetAll()
        {
            return
                           await _unitOfWork.WholeSalerLocalitys.GetAll();
        }

       /* public async Task Delete(WholeSalerLocality WholeSalerLocality)
        {
            _unitOfWork.WholeSalerLocalitys.Remove(WholeSalerLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WholeSalerLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WholeSalerLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<WholeSalerLocality> GetById(int id)
        {
            return
                await _unitOfWork.WholeSalerLocalitys.GetById(id);
        }
   
        public async Task Update(WholeSalerLocality WholeSalerLocalityToBeUpdated, WholeSalerLocality WholeSalerLocality)
        {
            WholeSalerLocality.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(WholeSalerLocality WholeSalerLocality)
        {
            //WholeSalerLocality musi =  _unitOfWork.WholeSalerLocalitys.SingleOrDefaultAsync(x=>x.Id == WholeSalerLocalityToBeUpdated.Id);
            WholeSalerLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<WholeSalerLocality> WholeSalerLocality)
        {
            foreach (var item in WholeSalerLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.WholeSalerLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.WholeSalerLocalitys.GetAllInActif();
        }
        //public Task<WholeSalerLocality> CreateWholeSalerLocality(WholeSalerLocality newWholeSalerLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWholeSalerLocality(WholeSalerLocality WholeSalerLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WholeSalerLocality> GetWholeSalerLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WholeSalerLocality>> GetWholeSalerLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWholeSalerLocality(WholeSalerLocality WholeSalerLocalityToBeUpdated, WholeSalerLocality WholeSalerLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
