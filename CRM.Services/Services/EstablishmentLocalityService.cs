using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentLocalityService : IEstablishmentLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstablishmentLocality> Create(EstablishmentLocality newEstablishmentLocality)
        {

            await _unitOfWork.EstablishmentLocalitys.Add(newEstablishmentLocality);
            await _unitOfWork.CommitAsync();
            return newEstablishmentLocality;
        }
        public async Task<List<EstablishmentLocality>> CreateRange(List<EstablishmentLocality> newEstablishmentLocality)
        {

            await _unitOfWork.EstablishmentLocalitys.AddRange(newEstablishmentLocality);
            await _unitOfWork.CommitAsync();
            return newEstablishmentLocality;
        }
        public async Task<IEnumerable<EstablishmentLocality>> GetAll()
        {
            return
                           await _unitOfWork.EstablishmentLocalitys.GetAll();
        }

       /* public async Task Delete(EstablishmentLocality EstablishmentLocality)
        {
            _unitOfWork.EstablishmentLocalitys.Remove(EstablishmentLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<EstablishmentLocality> GetById(int id)
        {
            return
                await _unitOfWork.EstablishmentLocalitys.GetById(id);
        }
   
        public async Task Update(EstablishmentLocality EstablishmentLocalityToBeUpdated, EstablishmentLocality EstablishmentLocality)
        {
            EstablishmentLocality.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(EstablishmentLocality EstablishmentLocality)
        {
            //EstablishmentLocality musi =  _unitOfWork.EstablishmentLocalitys.SingleOrDefaultAsync(x=>x.Id == EstablishmentLocalityToBeUpdated.Id);
            EstablishmentLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<EstablishmentLocality> EstablishmentLocality)
        {
            foreach (var item in EstablishmentLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.EstablishmentLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.EstablishmentLocalitys.GetAllInActif();
        }
        //public Task<EstablishmentLocality> CreateEstablishmentLocality(EstablishmentLocality newEstablishmentLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentLocality(EstablishmentLocality EstablishmentLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentLocality> GetEstablishmentLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentLocality>> GetEstablishmentLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentLocality(EstablishmentLocality EstablishmentLocalityToBeUpdated, EstablishmentLocality EstablishmentLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
