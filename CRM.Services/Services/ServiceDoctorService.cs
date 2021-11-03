using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ServiceDoctorService : IServiceDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ServiceDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceDoctor> Create(ServiceDoctor newServiceDoctor)
        {

            await _unitOfWork.ServiceDoctors.Add(newServiceDoctor);
            await _unitOfWork.CommitAsync();
            return newServiceDoctor;
        }
        public async Task<List<ServiceDoctor>> CreateRange(List<ServiceDoctor> newServiceDoctor)
        {

            await _unitOfWork.ServiceDoctors.AddRange(newServiceDoctor);
            await _unitOfWork.CommitAsync();
            return newServiceDoctor;
        }
        public async Task<IEnumerable<ServiceDoctor>> GetAll()
        {
            return
                           await _unitOfWork.ServiceDoctors.GetAll();
        }

       /* public async Task Delete(ServiceDoctor ServiceDoctor)
        {
            _unitOfWork.ServiceDoctors.Remove(ServiceDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<ServiceDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.ServiceDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<ServiceDoctor> GetById(int id)
        {
            return
                await _unitOfWork.ServiceDoctors.GetById(id);
        }
   
        public async Task Update(ServiceDoctor ServiceDoctorToBeUpdated, ServiceDoctor ServiceDoctor)
        {
            ServiceDoctor.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ServiceDoctor ServiceDoctor)
        {
            //ServiceDoctor musi =  _unitOfWork.ServiceDoctors.SingleOrDefaultAsync(x=>x.Id == ServiceDoctorToBeUpdated.Id);
            ServiceDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ServiceDoctor> ServiceDoctor)
        {
            foreach (var item in ServiceDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.ServiceDoctors.GetAllActif();
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.ServiceDoctors.GetAllInActif();
        }
        //public Task<ServiceDoctor> CreateServiceDoctor(ServiceDoctor newServiceDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteServiceDoctor(ServiceDoctor ServiceDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ServiceDoctor> GetServiceDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ServiceDoctor>> GetServiceDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateServiceDoctor(ServiceDoctor ServiceDoctorToBeUpdated, ServiceDoctor ServiceDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
