using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentDoctorService : IEstablishmentDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstablishmentDoctor> Create(EstablishmentDoctor newEstablishmentDoctor)
        {

            await _unitOfWork.EstablishmentDoctors.Add(newEstablishmentDoctor);
            await _unitOfWork.CommitAsync();
            return newEstablishmentDoctor;
        }
        public async Task<List<EstablishmentDoctor>> CreateRange(List<EstablishmentDoctor> newEstablishmentDoctor)
        {

            await _unitOfWork.EstablishmentDoctors.AddRange(newEstablishmentDoctor);
            await _unitOfWork.CommitAsync();
            return newEstablishmentDoctor;
        }
        public async Task<IEnumerable<EstablishmentDoctor>> GetAll()
        {
            return
                           await _unitOfWork.EstablishmentDoctors.GetAll();
        }

       /* public async Task Delete(EstablishmentDoctor EstablishmentDoctor)
        {
            _unitOfWork.EstablishmentDoctors.Remove(EstablishmentDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<EstablishmentDoctor> GetById(int id)
        {
            return
                await _unitOfWork.EstablishmentDoctors.GetById(id);
        }
   
        public async Task Update(EstablishmentDoctor EstablishmentDoctorToBeUpdated, EstablishmentDoctor EstablishmentDoctor)
        {
            EstablishmentDoctor.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(EstablishmentDoctor EstablishmentDoctor)
        {
            //EstablishmentDoctor musi =  _unitOfWork.EstablishmentDoctors.SingleOrDefaultAsync(x=>x.Id == EstablishmentDoctorToBeUpdated.Id);
            EstablishmentDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<EstablishmentDoctor> EstablishmentDoctor)
        {
            foreach (var item in EstablishmentDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentDoctors.GetAllActif();
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.EstablishmentDoctors.GetAllInActif();
        }
        //public Task<EstablishmentDoctor> CreateEstablishmentDoctor(EstablishmentDoctor newEstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentDoctor(EstablishmentDoctor EstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentDoctor> GetEstablishmentDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentDoctor>> GetEstablishmentDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentDoctor(EstablishmentDoctor EstablishmentDoctorToBeUpdated, EstablishmentDoctor EstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
