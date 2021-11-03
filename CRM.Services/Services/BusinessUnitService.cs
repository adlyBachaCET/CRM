
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class BusinessUnitService : IBusinessUnitService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BusinessUnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BusinessUnit> Create(BusinessUnit newBusinessUnit)
        {

            await _unitOfWork.BusinessUnits.Add(newBusinessUnit);
            await _unitOfWork.CommitAsync();
            return newBusinessUnit;
        }
        public async Task<List<BusinessUnit>> CreateRange(List<BusinessUnit> newBusinessUnit)
        {

            await _unitOfWork.BusinessUnits.AddRange(newBusinessUnit);
            await _unitOfWork.CommitAsync();
            return newBusinessUnit;
        }
        public async Task<IEnumerable<BusinessUnit>> GetAll()
        {
            return
                           await _unitOfWork.BusinessUnits.GetAll();
        }

       /* public async Task Delete(BusinessUnit BusinessUnit)
        {
            _unitOfWork.BusinessUnits.Remove(BusinessUnit);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<BusinessUnit>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.BusinessUnits
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<BusinessUnit> GetById(int id)
        {
            return
                await _unitOfWork.BusinessUnits.SingleOrDefault(i=>i.IdBu==id&& i.Active==0 );
        }
   
        public async Task Update(BusinessUnit BusinessUnitToBeUpdated, BusinessUnit BusinessUnit)
        {
            BusinessUnitToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            BusinessUnit.Version = BusinessUnitToBeUpdated.Version + 1;
            BusinessUnit.IdBu = BusinessUnitToBeUpdated.IdBu;
            BusinessUnit.Status = Status.Pending;
            BusinessUnit.Active = 1;

            await _unitOfWork.BusinessUnits.Add(BusinessUnit);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(BusinessUnit BusinessUnit)
        {
            //BusinessUnit musi =  _unitOfWork.BusinessUnits.SingleOrDefaultAsync(x=>x.Id == BusinessUnitToBeUpdated.Id);
            BusinessUnit.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<BusinessUnit> BusinessUnit)
        {
            foreach (var item in BusinessUnit)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllActif()
        {
            return
                             await _unitOfWork.BusinessUnits.GetAllActif();
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllInActif()
        {
            return
                             await _unitOfWork.BusinessUnits.GetAllInActif();
        }
        //public Task<BusinessUnit> CreateBusinessUnit(BusinessUnit newBusinessUnit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteBusinessUnit(BusinessUnit BusinessUnit)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<BusinessUnit> GetBusinessUnitById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<BusinessUnit>> GetBusinessUnitsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateBusinessUnit(BusinessUnit BusinessUnitToBeUpdated, BusinessUnit BusinessUnit)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
