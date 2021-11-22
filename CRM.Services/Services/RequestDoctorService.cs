using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class RequestDoctorService : IRequestDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public RequestDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestDoctor> Create(RequestDoctor newRequestDoctor)
        {

            await _unitOfWork.RequestDoctors.Add(newRequestDoctor);
            await _unitOfWork.CommitAsync();
            return newRequestDoctor;
        }
        public async Task<List<RequestDoctor>> CreateRange(List<RequestDoctor> newRequestDoctor)
        {

            await _unitOfWork.RequestDoctors.AddRange(newRequestDoctor);
            await _unitOfWork.CommitAsync();
            return newRequestDoctor;
        }
        public async Task<IEnumerable<RequestDoctor>> GetAll()
        {
            return
                           await _unitOfWork.RequestDoctors.GetAll();
        }

       /* public async Task Delete(RequestDoctor RequestDoctor)
        {
            _unitOfWork.RequestDoctors.Remove(RequestDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<RequestDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.RequestDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<RequestDoctor> GetById(int id)
        {
             return
               await _unitOfWork.RequestDoctors.SingleOrDefault(i => i.IdRequestDoctor == id && i.Active == 0);
        }
   
        public async Task Update(RequestDoctor RequestDoctorToBeUpdated, RequestDoctor RequestDoctor)
        {
            RequestDoctorToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            RequestDoctor.Version = RequestDoctorToBeUpdated.Version + 1;
            RequestDoctor.IdRequestDoctor = RequestDoctorToBeUpdated.IdRequestDoctor;
            RequestDoctor.Status = Status.Pending;
            RequestDoctor.Active = 0;

            await _unitOfWork.RequestDoctors.Add(RequestDoctor);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(RequestDoctor RequestDoctor)
        {
            //RequestDoctor musi =  _unitOfWork.RequestDoctors.SingleOrDefaultAsync(x=>x.Id == RequestDoctorToBeUpdated.Id);
            RequestDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<RequestDoctor> RequestDoctor)
        {
            foreach (var item in RequestDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<RequestDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.RequestDoctors.GetAllActif();
        }

        public async Task<IEnumerable<RequestDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.RequestDoctors.GetAllInActif();
        }

        public async Task<IEnumerable<RequestDoctor>> GetByIdDoctor(int id)
        {
            return
                            await _unitOfWork.RequestDoctors.GetByIdDoctor(id);
        }

        public async Task<IEnumerable<RequestDoctor>> GetByIdActifDoctor(int Id)
        {
            return
                  await _unitOfWork.RequestDoctors.GetByIdActifDoctor(Id);
        }
        public async Task<IEnumerable<RequestDoctor>> GetByIdActifPharmacy(int Id)
        {
            return
                  await _unitOfWork.RequestDoctors.GetByIdActifPharmacy(Id);
        }
        public async Task<IEnumerable<RequestDoctor>> GetByIdActifUser(int Id)
        {
            return
                  await _unitOfWork.RequestDoctors.GetByIdActifUser(Id);
        }
        //public Task<RequestDoctor> CreateRequestDoctor(RequestDoctor newRequestDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteRequestDoctor(RequestDoctor RequestDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<RequestDoctor> GetRequestDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<RequestDoctor>> GetRequestDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateRequestDoctor(RequestDoctor RequestDoctorToBeUpdated, RequestDoctor RequestDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
