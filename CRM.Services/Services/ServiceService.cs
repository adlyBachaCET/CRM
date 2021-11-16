using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ServiceService : IServiceService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Service> Create(Service newService)
        {

            await _unitOfWork.Services.Add(newService);
            await _unitOfWork.CommitAsync();
            return newService;
        }
        public async Task<List<Service>> CreateRange(List<Service> newService)
        {

            await _unitOfWork.Services.AddRange(newService);
            await _unitOfWork.CommitAsync();
            return newService;
        }
        public async Task<IEnumerable<Service>> GetAll()
        {
            return
                           await _unitOfWork.Services.GetAll();
        }

       /* public async Task Delete(Service Service)
        {
            _unitOfWork.Services.Remove(Service);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Service>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Services
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Service> GetById(int? id)
        {
            return
                      await _unitOfWork.Services.SingleOrDefault(i => i.IdService == id && i.Active == 0);
        }
   
        public async Task Update(Service ServiceToBeUpdated, Service Service)
        {
            ServiceToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Service.Version = ServiceToBeUpdated.Version + 1;
            Service.IdService = ServiceToBeUpdated.IdService;
            Service.Status = Status.Pending;
            Service.Active = 0;

            await _unitOfWork.Services.Add(Service);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Service Service)
        {
            //Service musi =  _unitOfWork.Services.SingleOrDefaultAsync(x=>x.Id == ServiceToBeUpdated.Id);
            Service.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Service> Service)
        {
            foreach (var item in Service)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Service>> GetAllActif()
        {
            return
                             await _unitOfWork.Services.GetAllActif();
        }

        public async Task<IEnumerable<Service>> GetAllInActif()
        {
            return
                             await _unitOfWork.Services.GetAllInActif();
        }

        public async Task<Service> GetByNameActif(string Name)
        {
            return
                                      await _unitOfWork.Services.GetByNameActif(Name);
        }
        //public Task<Service> CreateService(Service newService)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteService(Service Service)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Service> GetServiceById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Service>> GetServicesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateService(Service ServiceToBeUpdated, Service Service)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
