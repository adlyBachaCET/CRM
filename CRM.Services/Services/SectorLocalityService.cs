using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SectorLocalityService : ISectorLocalityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SectorLocalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SectorLocality> Create(SectorLocality newSectorLocality)
        {

            await _unitOfWork.SectorLocalitys.Add(newSectorLocality);
            await _unitOfWork.CommitAsync();
            return newSectorLocality;
        }
        public async Task<List<SectorLocality>> CreateRange(List<SectorLocality> newSectorLocality)
        {

            await _unitOfWork.SectorLocalitys.AddRange(newSectorLocality);
            await _unitOfWork.CommitAsync();
            return newSectorLocality;
        }
        public async Task<IEnumerable<SectorLocality>> GetAll()
        {
            return
                           await _unitOfWork.SectorLocalitys.GetAll();
        }

       /* public async Task Delete(SectorLocality SectorLocality)
        {
            _unitOfWork.SectorLocalitys.Remove(SectorLocality);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<SectorLocality>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.SectorLocalitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<SectorLocality> GetById(int id)
        {
            return
                await _unitOfWork.SectorLocalitys.GetById(id);
        }
   
        public async Task Update(SectorLocality SectorLocalityToBeUpdated, SectorLocality SectorLocality)
        {
            SectorLocality.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SectorLocality SectorLocality)
        {
            //SectorLocality musi =  _unitOfWork.SectorLocalitys.SingleOrDefaultAsync(x=>x.Id == SectorLocalityToBeUpdated.Id);
            SectorLocality.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SectorLocality> SectorLocality)
        {
            foreach (var item in SectorLocality)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SectorLocality>> GetAllActif()
        {
            return
                             await _unitOfWork.SectorLocalitys.GetAllActif();
        }

        public async Task<IEnumerable<SectorLocality>> GetAllInActif()
        {
            return
                             await _unitOfWork.SectorLocalitys.GetAllInActif();
        }
        //public Task<SectorLocality> CreateSectorLocality(SectorLocality newSectorLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSectorLocality(SectorLocality SectorLocality)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<SectorLocality> GetSectorLocalityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<SectorLocality>> GetSectorLocalitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSectorLocality(SectorLocality SectorLocalityToBeUpdated, SectorLocality SectorLocality)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
