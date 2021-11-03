
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentServiceService : IEstablishmentServiceService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstablishmentService> Create(EstablishmentService newEstablishmentService)
        {

            await _unitOfWork.EstablishmentServices.Add(newEstablishmentService);
            await _unitOfWork.CommitAsync();
            return newEstablishmentService;
        }
        public async Task<List<EstablishmentService>> CreateRange(List<EstablishmentService> newEstablishmentService)
        {

            await _unitOfWork.EstablishmentServices.AddRange(newEstablishmentService);
            await _unitOfWork.CommitAsync();
            return newEstablishmentService;
        }
        public async Task<IEnumerable<EstablishmentService>> GetAll()
        {
            return
                           await _unitOfWork.EstablishmentServices.GetAll();
        }

       /* public async Task Delete(EstablishmentService EstablishmentService)
        {
            _unitOfWork.EstablishmentServices.Remove(EstablishmentService);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentService>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentServices
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<EstablishmentService> GetById(int id)
        {
            return
                await _unitOfWork.EstablishmentServices.GetById(id);
        }
   
        public async Task Update(EstablishmentService EstablishmentServiceToBeUpdated, EstablishmentService EstablishmentService)
        {
            EstablishmentService.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(EstablishmentService EstablishmentService)
        {
            //EstablishmentService musi =  _unitOfWork.EstablishmentServices.SingleOrDefaultAsync(x=>x.Id == EstablishmentServiceToBeUpdated.Id);
            EstablishmentService.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<EstablishmentService> EstablishmentService)
        {
            foreach (var item in EstablishmentService)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentServices.GetAllActif();
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllInActif()
        {
            return
                             await _unitOfWork.EstablishmentServices.GetAllInActif();
        }
        //public Task<EstablishmentService> CreateEstablishmentService(EstablishmentService newEstablishmentService)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentService(EstablishmentService EstablishmentService)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentService> GetEstablishmentServiceById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentService>> GetEstablishmentServicesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentService(EstablishmentService EstablishmentServiceToBeUpdated, EstablishmentService EstablishmentService)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
