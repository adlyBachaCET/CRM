using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PharmacyLocalityService : IPharmacyLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PharmacyLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PharmacyLocality> Create(PharmacyLocality newPharmacyLocality)
        {

            await _unitOfWork.PharmacyLocalitys.Add(newPharmacyLocality);
            await _unitOfWork.CommitAsync();
            return newPharmacyLocality;
        }
        public async Task<List<PharmacyLocality>> CreateRange(List<PharmacyLocality> newPharmacyLocality)
        {

            await _unitOfWork.PharmacyLocalitys.AddRange(newPharmacyLocality);
            await _unitOfWork.CommitAsync();
            return newPharmacyLocality;
        }
        public async Task<IEnumerable<PharmacyLocality>> GetAll()
        {
            return
                           await _unitOfWork.PharmacyLocalitys.GetAll();
        }

       /* public async Task Delete(PharmacyLocality PharmacyLocality)
        {
            _unitOfWork.PharmacyLocalitys.Remove(PharmacyLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<PharmacyLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.PharmacyLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<PharmacyLocality> GetById(int id)
        {
            return
                await _unitOfWork.PharmacyLocalitys.GetById(id);
        }
   
        public async Task Update(PharmacyLocality PharmacyLocalityToBeUpdated, PharmacyLocality PharmacyLocality)
        {
            PharmacyLocality.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(PharmacyLocality PharmacyLocality)
        {
            //PharmacyLocality musi =  _unitOfWork.PharmacyLocalitys.SingleOrDefaultAsync(x=>x.Id == PharmacyLocalityToBeUpdated.Id);
            PharmacyLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<PharmacyLocality> PharmacyLocality)
        {
            foreach (var item in PharmacyLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.PharmacyLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.PharmacyLocalitys.GetAllInActif();
        }
        //public Task<PharmacyLocality> CreatePharmacyLocality(PharmacyLocality newPharmacyLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePharmacyLocality(PharmacyLocality PharmacyLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<PharmacyLocality> GetPharmacyLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<PharmacyLocality>> GetPharmacyLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePharmacyLocality(PharmacyLocality PharmacyLocalityToBeUpdated, PharmacyLocality PharmacyLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
