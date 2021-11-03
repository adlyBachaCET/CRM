using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PharmacyService : IPharmacyService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PharmacyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pharmacy> Create(Pharmacy newPharmacy)
        {

            await _unitOfWork.Pharmacys.Add(newPharmacy);
            await _unitOfWork.CommitAsync();
            return newPharmacy;
        }
        public async Task<List<Pharmacy>> CreateRange(List<Pharmacy> newPharmacy)
        {

            await _unitOfWork.Pharmacys.AddRange(newPharmacy);
            await _unitOfWork.CommitAsync();
            return newPharmacy;
        }
        public async Task<IEnumerable<Pharmacy>> GetAll()
        {
            return
                           await _unitOfWork.Pharmacys.GetAll();
        }

       /* public async Task Delete(Pharmacy Pharmacy)
        {
            _unitOfWork.Pharmacys.Remove(Pharmacy);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Pharmacy>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Pharmacys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Pharmacy> GetById(int id)
        {
            return
                      await _unitOfWork.Pharmacys.SingleOrDefault(i => i.IdPharmacy == id && i.Active == 0);
        }
   
        public async Task Update(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy)
        {
            PharmacyToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            Pharmacy.Version = PharmacyToBeUpdated.Version + 1;
            Pharmacy.IdPharmacy = PharmacyToBeUpdated.IdPharmacy;
            Pharmacy.Status = Status.Pending;
            Pharmacy.Active = 1;

            await _unitOfWork.Pharmacys.Add(Pharmacy);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Pharmacy Pharmacy)
        {
            //Pharmacy musi =  _unitOfWork.Pharmacys.SingleOrDefaultAsync(x=>x.Id == PharmacyToBeUpdated.Id);
            Pharmacy.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Pharmacy> Pharmacy)
        {
            foreach (var item in Pharmacy)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Pharmacy>> GetAllActif()
        {
            return
                             await _unitOfWork.Pharmacys.GetAllActif();
        }

        public async Task<IEnumerable<Pharmacy>> GetAllInActif()
        {
            return
                             await _unitOfWork.Pharmacys.GetAllInActif();
        }
        //public Task<Pharmacy> CreatePharmacy(Pharmacy newPharmacy)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePharmacy(Pharmacy Pharmacy)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Pharmacy> GetPharmacyById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Pharmacy>> GetPharmacysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePharmacy(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
