using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class WholeSaleservice : IWholeSalerService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public WholeSaleservice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WholeSaler> Create(WholeSaler newWholeSaler)
        {

            await _unitOfWork.WholeSales.Add(newWholeSaler);
            await _unitOfWork.CommitAsync();
            return newWholeSaler;
        }
        public async Task<List<WholeSaler>> CreateRange(List<WholeSaler> newWholeSaler)
        {

            await _unitOfWork.WholeSales.AddRange(newWholeSaler);
            await _unitOfWork.CommitAsync();
            return newWholeSaler;
        }
        public async Task<IEnumerable<WholeSaler>> GetAll()
        {
            return
                           await _unitOfWork.WholeSales.GetAll();
        }

       /* public async Task Delete(WholeSaler WholeSaler)
        {
            _unitOfWork.WholeSales.Remove(WholeSaler);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WholeSaler>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WholeSales
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<WholeSaler> GetById(int id)
        {

            return
                  await _unitOfWork.WholeSales.SingleOrDefault(i => i.IdPwholeSaler == id && i.Active == 0);
        }
   
        public async Task Update(WholeSaler WholeSalerToBeUpdated, WholeSaler WholeSaler)
        {
            WholeSalerToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            WholeSaler.Version = WholeSalerToBeUpdated.Version + 1;
            WholeSaler.IdPwholeSaler = WholeSalerToBeUpdated.IdPwholeSaler;
            WholeSaler.Status = Status.Pending;
            WholeSaler.Active = 1;

            await _unitOfWork.WholeSales.Add(WholeSaler);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(WholeSaler WholeSaler)
        {
            //WholeSaler musi =  _unitOfWork.WholeSales.SingleOrDefaultAsync(x=>x.Id == WholeSalerToBeUpdated.Id);
            WholeSaler.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<WholeSaler> WholeSaler)
        {
            foreach (var item in WholeSaler)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<WholeSaler>> GetAllActif()
        {
            return
                             await _unitOfWork.WholeSales.GetAllActif();
        }

        public async Task<IEnumerable<WholeSaler>> GetAllInActif()
        {
            return
                             await _unitOfWork.WholeSales.GetAllInActif();
        }
        //public Task<WholeSaler> CreateWholeSaler(WholeSaler newWholeSaler)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWholeSaler(WholeSaler WholeSaler)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WholeSaler> GetWholeSalerById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WholeSaler>> GetWholeSalesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWholeSaler(WholeSaler WholeSalerToBeUpdated, WholeSaler WholeSaler)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
