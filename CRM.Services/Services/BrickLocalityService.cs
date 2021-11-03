using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class BrickLocalityService : IBrickLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BrickLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BrickLocality> Create(BrickLocality newBrickLocality)
        {
            newBrickLocality.Version = 1;
            await _unitOfWork.BrickLocalitys.Add(newBrickLocality);
            await _unitOfWork.CommitAsync();
            return newBrickLocality;
        }
        public async Task<List<BrickLocality>> CreateRange(List<BrickLocality> newBrickLocality)
        {

            await _unitOfWork.BrickLocalitys.AddRange(newBrickLocality);
            await _unitOfWork.CommitAsync();
            return newBrickLocality;
        }
        public async Task<IEnumerable<BrickLocality>> GetAll()
        {
            return
                           await _unitOfWork.BrickLocalitys.GetAll();
        }

       /* public async Task Delete(BrickLocality BrickLocality)
        {
            _unitOfWork.BrickLocalitys.Remove(BrickLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<BrickLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.BrickLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<BrickLocality> GetById(int id)
        {
            return
                await _unitOfWork.BrickLocalitys.GetById(id);
        }
   
        public async Task Update(BrickLocality BrickLocalityToBeUpdated, BrickLocality BrickLocality)
        {
            BrickLocalityToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            BrickLocality.Version = BrickLocalityToBeUpdated.Version + 1;
            BrickLocality.IdBrick = BrickLocalityToBeUpdated.IdBrick;
            BrickLocality.Status = Status.Pending;
            BrickLocality.Active = 1;

            await _unitOfWork.BrickLocalitys.Add(BrickLocality);
            await _unitOfWork.CommitAsync();

        }

        public async Task Delete(BrickLocality BrickLocality)
        {
            //BrickLocality musi =  _unitOfWork.BrickLocalitys.SingleOrDefaultAsync(x=>x.Id == BrickLocalityToBeUpdated.Id);
            BrickLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<BrickLocality> BrickLocality)
        {
            foreach (var item in BrickLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<BrickLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.BrickLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<BrickLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.BrickLocalitys.GetAllInActif();
        }
        //public Task<BrickLocality> CreateBrickLocality(BrickLocality newBrickLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteBrickLocality(BrickLocality BrickLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<BrickLocality> GetBrickLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<BrickLocality>> GetBrickLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateBrickLocality(BrickLocality BrickLocalityToBeUpdated, BrickLocality BrickLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
