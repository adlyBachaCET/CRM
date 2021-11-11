using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class LocationTypeService : ILocationTypeService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public LocationTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LocationType> Create(LocationType newEstablishmentType)
        {

            await _unitOfWork.EstablishmentTypes.Add(newEstablishmentType);
            await _unitOfWork.CommitAsync();
            return newEstablishmentType;
        }
        public async Task<List<LocationType>> CreateRange(List<LocationType> newEstablishmentType)
        {

            await _unitOfWork.EstablishmentTypes.AddRange(newEstablishmentType);
            await _unitOfWork.CommitAsync();
            return newEstablishmentType;
        }
        public async Task<IEnumerable<LocationType>> GetAll()
        {
            return
                           await _unitOfWork.EstablishmentTypes.GetAll();
        }

       /* public async Task Delete(EstablishmentType EstablishmentType)
        {
            _unitOfWork.EstablishmentTypes.Remove(EstablishmentType);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentType>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentTypes
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<LocationType> GetById(int? id)
        {
            return
                  await _unitOfWork.EstablishmentTypes.SingleOrDefault(i => i.IdLocationType == id && i.Active == 0);
        }
   
        public async Task Update(LocationType EstablishmentTypeToBeUpdated, LocationType EstablishmentType)
        {
            EstablishmentTypeToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            EstablishmentType.Version = EstablishmentTypeToBeUpdated.Version + 1;
            EstablishmentType.IdLocationType = EstablishmentTypeToBeUpdated.IdLocationType;
            EstablishmentType.Status = Status.Pending;
            EstablishmentType.Active = 0;

            await _unitOfWork.EstablishmentTypes.Add(EstablishmentType);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(LocationType EstablishmentType)
        {
            //EstablishmentType musi =  _unitOfWork.EstablishmentTypes.SingleOrDefaultAsync(x=>x.Id == EstablishmentTypeToBeUpdated.Id);
            EstablishmentType.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<LocationType> EstablishmentType)
        {
            foreach (var item in EstablishmentType)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<LocationType>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentTypes.GetAllActif();
        }

        public async Task<IEnumerable<LocationType>> GetAllInActif()
        {
            return
                             await _unitOfWork.EstablishmentTypes.GetAllInActif();
        }
        //public Task<EstablishmentType> CreateEstablishmentType(EstablishmentType newEstablishmentType)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentType(EstablishmentType EstablishmentType)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentType> GetEstablishmentTypeById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentType>> GetEstablishmentTypesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentType(EstablishmentType EstablishmentTypeToBeUpdated, EstablishmentType EstablishmentType)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
