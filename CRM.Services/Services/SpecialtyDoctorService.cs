using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SpecialtyDoctorService : ISpecialtyDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SpecialtyDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SpecialtyDoctor> Create(SpecialtyDoctor newSpecialtyDoctor)
        {

            await _unitOfWork.SpecialtyDoctors.Add(newSpecialtyDoctor);
            await _unitOfWork.CommitAsync();
            return newSpecialtyDoctor;
        }
        public async Task<List<SpecialtyDoctor>> CreateRange(List<SpecialtyDoctor> newSpecialtyDoctor)
        {

            await _unitOfWork.SpecialtyDoctors.AddRange(newSpecialtyDoctor);
            await _unitOfWork.CommitAsync();
            return newSpecialtyDoctor;
        }
        public async Task<IEnumerable<SpecialtyDoctor>> GetAll()
        {
            return
                           await _unitOfWork.SpecialtyDoctors.GetAll();
        }

       /* public async Task Delete(SpecialtyDoctor SpecialtyDoctor)
        {
            _unitOfWork.SpecialtyDoctors.Remove(SpecialtyDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<SpecialtyDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.SpecialtyDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<SpecialtyDoctor> GetById(int id)
        {
            return
                await _unitOfWork.SpecialtyDoctors.GetById(id);
        }
        public async Task<SpecialtyDoctor> GetById(int idDoctor,int idSpecialty)
        {
            return
                await _unitOfWork.SpecialtyDoctors.GetById( idDoctor,  idSpecialty);
        }
   
        public async Task Update(SpecialtyDoctor SpecialtyDoctorToBeUpdated, SpecialtyDoctor SpecialtyDoctor)
        {
            SpecialtyDoctor.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SpecialtyDoctor SpecialtyDoctor)
        {
            //SpecialtyDoctor musi =  _unitOfWork.SpecialtyDoctors.SingleOrDefaultAsync(x=>x.Id == SpecialtyDoctorToBeUpdated.Id);
            SpecialtyDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SpecialtyDoctor> SpecialtyDoctor)
        {
            foreach (var item in SpecialtyDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.SpecialtyDoctors.GetAllActif();
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.SpecialtyDoctors.GetAllInActif();
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetByIdDoctor(int id)
        {
            return await _unitOfWork.SpecialtyDoctors.Find(i => i.IdDoctor == id&& i.Active==0);
        }


        //public Task<SpecialtyDoctor> CreateSpecialtyDoctor(SpecialtyDoctor newSpecialtyDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSpecialtyDoctor(SpecialtyDoctor SpecialtyDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<SpecialtyDoctor> GetSpecialtyDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<SpecialtyDoctor>> GetSpecialtyDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSpecialtyDoctor(SpecialtyDoctor SpecialtyDoctorToBeUpdated, SpecialtyDoctor SpecialtyDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
