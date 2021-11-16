using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class BuDoctorService : IBuDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BuDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BuDoctor> Create(BuDoctor newBuDoctor)
        {

            await _unitOfWork.BuDoctors.Add(newBuDoctor);
            await _unitOfWork.CommitAsync();
            return newBuDoctor;
        }
        public async Task<List<BuDoctor>> CreateRange(List<BuDoctor> newBuDoctor)
        {

            await _unitOfWork.BuDoctors.AddRange(newBuDoctor);
            await _unitOfWork.CommitAsync();
            return newBuDoctor;
        }
        public async Task<IEnumerable<BuDoctor>> GetAll()
        {
            return
                           await _unitOfWork.BuDoctors.GetAll();
        }

       /* public async Task Delete(BuDoctor BuDoctor)
        {
            _unitOfWork.BuDoctors.Remove(BuDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<BuDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.BuDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<BuDoctor> GetById(int id)
        {
            return
                await _unitOfWork.BuDoctors.GetById(id);
        }
   
        public async Task Update(BuDoctor BuDoctorToBeUpdated, BuDoctor BuDoctor)
        {
            BuDoctor.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(BuDoctor BuDoctor)
        {
            //BuDoctor musi =  _unitOfWork.BuDoctors.SingleOrDefaultAsync(x=>x.Id == BuDoctorToBeUpdated.Id);
            BuDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<BuDoctor> BuDoctor)
        {
            foreach (var item in BuDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<BuDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.BuDoctors.GetAllActif();
        }

        public async Task<IEnumerable<BuDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.BuDoctors.GetAllInActif();
        }

        public async Task<IEnumerable<BuDoctor>> GetByIdDoctor(int id)
        {
            return await _unitOfWork.BuDoctors.Find(i => i.IdDoctor == id&& i.Active==0);
        }


        //public Task<BuDoctor> CreateBuDoctor(BuDoctor newBuDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteBuDoctor(BuDoctor BuDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<BuDoctor> GetBuDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<BuDoctor>> GetBuDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateBuDoctor(BuDoctor BuDoctorToBeUpdated, BuDoctor BuDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
