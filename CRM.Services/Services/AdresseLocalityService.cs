using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class AdresseLocalityService : IAdresseLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public AdresseLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AdresseLocality> Create(AdresseLocality newAdresseLocality)
        {

            await _unitOfWork.AdresseLocalitys.Add(newAdresseLocality);
            await _unitOfWork.CommitAsync();
            return newAdresseLocality;
        }
        public async Task<List<AdresseLocality>> CreateRange(List<AdresseLocality> newAdresseLocality)
        {

            await _unitOfWork.AdresseLocalitys.AddRange(newAdresseLocality);
            await _unitOfWork.CommitAsync();
            return newAdresseLocality;
        }
        public async Task<IEnumerable<AdresseLocality>> GetAll()
        {
            return
                           await _unitOfWork.AdresseLocalitys.GetAll();
        }

       /* public async Task Delete(AdresseLocality AdresseLocality)
        {
            _unitOfWork.AdresseLocalitys.Remove(AdresseLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<AdresseLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.AdresseLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<AdresseLocality> GetById(int id)
        {
            return
                await _unitOfWork.AdresseLocalitys.GetById(id);
        }
   
        public async Task Update(AdresseLocality AdresseLocalityToBeUpdated, AdresseLocality AdresseLocality)
        {
            AdresseLocality.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(AdresseLocality AdresseLocality)
        {
            //AdresseLocality musi =  _unitOfWork.AdresseLocalitys.SingleOrDefaultAsync(x=>x.Id == AdresseLocalityToBeUpdated.Id);
            AdresseLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<AdresseLocality> AdresseLocality)
        {
            foreach (var item in AdresseLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.AdresseLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.AdresseLocalitys.GetAllInActif();
        }
        //public Task<AdresseLocality> CreateAdresseLocality(AdresseLocality newAdresseLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteAdresseLocality(AdresseLocality AdresseLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<AdresseLocality> GetAdresseLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<AdresseLocality>> GetAdresseLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAdresseLocality(AdresseLocality AdresseLocalityToBeUpdated, AdresseLocality AdresseLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
