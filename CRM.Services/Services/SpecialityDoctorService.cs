using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SpecialityDoctorService : ISpecialityDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SpecialityDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SpecialityDoctor> Create(SpecialityDoctor newSpecialityDoctor)
        {

            await _unitOfWork.SpecialityDoctors.Add(newSpecialityDoctor);
            await _unitOfWork.CommitAsync();
            return newSpecialityDoctor;
        }
        public async Task<List<SpecialityDoctor>> CreateRange(List<SpecialityDoctor> newSpecialityDoctor)
        {

            await _unitOfWork.SpecialityDoctors.AddRange(newSpecialityDoctor);
            await _unitOfWork.CommitAsync();
            return newSpecialityDoctor;
        }
        public async Task<IEnumerable<SpecialityDoctor>> GetAll()
        {
            return
                           await _unitOfWork.SpecialityDoctors.GetAll();
        }

       /* public async Task Delete(SpecialityDoctor SpecialityDoctor)
        {
            _unitOfWork.SpecialityDoctors.Remove(SpecialityDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<SpecialityDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.SpecialityDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<SpecialityDoctor> GetById(int id)
        {
            return
                await _unitOfWork.SpecialityDoctors.GetById(id);
        }
   
        public async Task Update(SpecialityDoctor SpecialityDoctorToBeUpdated, SpecialityDoctor SpecialityDoctor)
        {
            SpecialityDoctorToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            SpecialityDoctor.Version = SpecialityDoctorToBeUpdated.Version + 1;
            SpecialityDoctor.IdSpecialty = SpecialityDoctorToBeUpdated.IdSpecialty;
            SpecialityDoctor.Status = Status.Pending;
            SpecialityDoctor.Active = 1;

            await _unitOfWork.SpecialityDoctors.Add(SpecialityDoctor);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SpecialityDoctor SpecialityDoctor)
        {
            //SpecialityDoctor musi =  _unitOfWork.SpecialityDoctors.SingleOrDefaultAsync(x=>x.Id == SpecialityDoctorToBeUpdated.Id);
            SpecialityDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SpecialityDoctor> SpecialityDoctor)
        {
            foreach (var item in SpecialityDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.SpecialityDoctors.GetAllActif();
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.SpecialityDoctors.GetAllInActif();
        }
        //public Task<SpecialityDoctor> CreateSpecialityDoctor(SpecialityDoctor newSpecialityDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSpecialityDoctor(SpecialityDoctor SpecialityDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<SpecialityDoctor> GetSpecialityDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<SpecialityDoctor>> GetSpecialityDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSpecialityDoctor(SpecialityDoctor SpecialityDoctorToBeUpdated, SpecialityDoctor SpecialityDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
