
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PhoneService : IPhoneService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PhoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Phone> Create(Phone newPhone)
        {

            await _unitOfWork.Phones.Add(newPhone);
            await _unitOfWork.CommitAsync();
            return newPhone;
        }
        public async Task<List<Phone>> CreateRange(List<Phone> newPhone)
        {

            await _unitOfWork.Phones.AddRange(newPhone);
            await _unitOfWork.CommitAsync();
            return newPhone;
        }
        public async Task<IEnumerable<Phone>> GetAll()
        {
            return
                           await _unitOfWork.Phones.GetAll();
        }

        /* public async Task Delete(Phone Phone)
         {
             _unitOfWork.Phones.Remove(Phone);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Phone>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Phones
        //          .GetAllWithArtisteAsync();
        //}
   
        public async Task<Phone> GetById(int id)
        {
            return
               await _unitOfWork.Phones.SingleOrDefault(i => i.IdPhone == id && i.Active == 0);
        }
        public async Task<Phone> GetByNumber(int Num)
        {
            return
               await _unitOfWork.Phones.SingleOrDefault(i => i.PhoneNumber == Num && i.Active == 0);
        }
     
        public async Task<IEnumerable<Phone>> GetByIdPharmacy(int id)
        {
            return
               await _unitOfWork.Phones.Find(i => i.IdPharmacy == id && i.Active == 0);
        }
        public async Task<IEnumerable<Phone>> GetByIdWholeSaler(int id)
        {
            return
               await _unitOfWork.Phones.Find(i => i.IdWholeSaler == id && i.Active == 0);
        }
        public async Task<IEnumerable<Phone>> GetByIdDoctor(int id)
        {
            return
               await _unitOfWork.Phones.Find(i => i.IdDoctor == id && i.Active == 0);
        }
        public async Task<IEnumerable<Phone>> GetAllById(int id)
        {
            return
               await _unitOfWork.Phones.GetAllById(id);
        }
        public async Task Update(Phone PhoneToBeUpdated, Phone Phone)
        {
            PhoneToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Phone.Version = PhoneToBeUpdated.Version + 1;
            Phone.IdPhone = PhoneToBeUpdated.IdPhone;
            Phone.Status = Status.Pending;
            Phone.Active = 0;

            await _unitOfWork.Phones.Add(Phone);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Phone Phone)
        {
            //Phone musi =  _unitOfWork.Phones.SingleOrDefaultAsync(x=>x.Id == PhoneToBeUpdated.Id);
            Phone.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Phone> Phone)
        {
            foreach (var item in Phone)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Phone>> GetAllActif()
        {
            return
                             await _unitOfWork.Phones.GetAllActif();
        }

        public async Task<IEnumerable<Phone>> GetAllInActif()
        {
            return
                             await _unitOfWork.Phones.GetAllInActif();
        }


        //public Task<Phone> CreatePhone(Phone newPhone)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePhone(Phone Phone)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Phone> GetPhoneById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Phone>> GetPhonesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePhone(Phone PhoneToBeUpdated, Phone Phone)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
