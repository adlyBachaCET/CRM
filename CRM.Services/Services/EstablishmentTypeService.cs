using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentTypeService : IEstablishmentTypeService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstablishmentType> Create(EstablishmentType newEstablishmentType)
        {

            await _unitOfWork.EstablishmentTypes.Add(newEstablishmentType);
            await _unitOfWork.CommitAsync();
            return newEstablishmentType;
        }
        public async Task<List<EstablishmentType>> CreateRange(List<EstablishmentType> newEstablishmentType)
        {

            await _unitOfWork.EstablishmentTypes.AddRange(newEstablishmentType);
            await _unitOfWork.CommitAsync();
            return newEstablishmentType;
        }
        public async Task<IEnumerable<EstablishmentType>> GetAll()
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

        public async Task<EstablishmentType> GetById(int id)
        {
            return
                  await _unitOfWork.EstablishmentTypes.SingleOrDefault(i => i.IdEstablishmentType == id && i.Active == 0);
        }
   
        public async Task Update(EstablishmentType EstablishmentTypeToBeUpdated, EstablishmentType EstablishmentType)
        {
            EstablishmentTypeToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            EstablishmentType.Version = EstablishmentTypeToBeUpdated.Version + 1;
            EstablishmentType.IdEstablishmentType = EstablishmentTypeToBeUpdated.IdEstablishmentType;
            EstablishmentType.Status = Status.Pending;
            EstablishmentType.Active = 1;

            await _unitOfWork.EstablishmentTypes.Add(EstablishmentType);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(EstablishmentType EstablishmentType)
        {
            //EstablishmentType musi =  _unitOfWork.EstablishmentTypes.SingleOrDefaultAsync(x=>x.Id == EstablishmentTypeToBeUpdated.Id);
            EstablishmentType.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<EstablishmentType> EstablishmentType)
        {
            foreach (var item in EstablishmentType)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentTypes.GetAllActif();
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllInActif()
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
