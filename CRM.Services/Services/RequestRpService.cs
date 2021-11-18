
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class RequestRpService : IRequestRpService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public RequestRpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestRp> Create(RequestRp newRequestRp)
        {

            await _unitOfWork.RequestRps.Add(newRequestRp);
            await _unitOfWork.CommitAsync();
            return newRequestRp;
        }
        public async Task<List<RequestRp>> CreateRange(List<RequestRp> newRequestRp)
        {

            await _unitOfWork.RequestRps.AddRange(newRequestRp);
            await _unitOfWork.CommitAsync();
            return newRequestRp;
        }
        public async Task<IEnumerable<RequestRp>> GetAll()
        {
            return
                           await _unitOfWork.RequestRps.GetAll();
        }

       /* public async Task Delete(RequestRp RequestRp)
        {
            _unitOfWork.RequestRps.Remove(RequestRp);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<RequestRp>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.RequestRps
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<RequestRp> GetById(int id)
        {
            return
                await _unitOfWork.RequestRps.GetByIdActif(id);
        }
   
        public async Task Update(RequestRp RequestRpToBeUpdated, RequestRp RequestRp)
        {
            RequestRpToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            RequestRp.Version = RequestRpToBeUpdated.Version + 1;
            RequestRp.IdRequestRp = RequestRpToBeUpdated.IdRequestRp;
            RequestRp.Status = Status.Pending;
            RequestRp.Active = 1;

            await _unitOfWork.RequestRps.Add(RequestRp);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(RequestRp RequestRp)
        {
            //RequestRp musi =  _unitOfWork.RequestRps.SingleOrDefaultAsync(x=>x.Id == RequestRpToBeUpdated.Id);
            RequestRp.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<RequestRp> RequestRp)
        {
            foreach (var item in RequestRp)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<RequestRp>> GetAllActif()
        {
            return
                             await _unitOfWork.RequestRps.GetAllActif();
        }

        public async Task<IEnumerable<RequestRp>> GetAllInActif()
        {
            return
                             await _unitOfWork.RequestRps.GetAllInActif();
        }

    
        //public Task<RequestRp> CreateRequestRp(RequestRp newRequestRp)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteRequestRp(RequestRp RequestRp)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<RequestRp> GetRequestRpById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<RequestRp>> GetRequestRpsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateRequestRp(RequestRp RequestRpToBeUpdated, RequestRp RequestRp)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
