using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class LocalityService : ILocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public LocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Locality> Create(Locality newLocality)
        {

            await _unitOfWork.Localitys.Add(newLocality);
            await _unitOfWork.CommitAsync();
            return newLocality;
        }
        public async Task<List<Locality>> CreateRange(List<Locality> newLocality)
        {

            await _unitOfWork.Localitys.AddRange(newLocality);
            await _unitOfWork.CommitAsync();
            return newLocality;
        }
        public async Task<IEnumerable<Locality>> GetAll()
        {
            return
                           await _unitOfWork.Localitys.GetAll();
        }

       /* public async Task Delete(Locality Locality)
        {
            _unitOfWork.Localitys.Remove(Locality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Locality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Localitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Locality> GetById(int id)
        {
            return
                 await _unitOfWork.Localitys.GetByIdActif(id);
        }
        public async Task<Locality> GetByIdActif(int? id)
        {
            return
                 await _unitOfWork.Localitys.GetByIdActif(id);
        }
        public async Task Update(Locality LocalityToBeUpdated, Locality Locality)
        {
            LocalityToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            Locality.Version = LocalityToBeUpdated.Version + 1;
            Locality.IdLocality = LocalityToBeUpdated.IdLocality;
            Locality.Status = Status.Pending;
            Locality.Active = 1;

            await _unitOfWork.Localitys.Add(Locality);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Locality Locality)
        {
            Locality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Locality> Locality)
        {
            foreach (var item in Locality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Locality>> GetAllActif()
        {
            return
                             await _unitOfWork.Localitys.GetAllActif();
        }

        public async Task<IEnumerable<Locality>> GetAllInActif()
        {
            return
                             await _unitOfWork.Localitys.GetAllInActif();
        }

        public async Task<Locality> GetByIdAndName(int? id, string Name)
        {
            return
                 await _unitOfWork.Localitys.SingleOrDefault(i => i.IdLocality == id && i.Active == 0 && i.Name== Name);
        }

        public async Task<IEnumerable<Locality>> GetAllActifLVL1()
        {
            return
                            await _unitOfWork.Localitys.GetAllActifLVL1();
        }

        public async Task<IEnumerable<Locality>> GetAllActifLVL2(int id)
        {
            return
                                       await _unitOfWork.Localitys.GetAllActifLVL2(id);
        }
        //public Task<Locality> CreateLocality(Locality newLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteLocality(Locality Locality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Locality> GetLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Locality>> GetLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateLocality(Locality LocalityToBeUpdated, Locality Locality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
