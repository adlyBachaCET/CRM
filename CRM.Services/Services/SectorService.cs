using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SectorService : ISectorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Sector> Create(Sector newSector)
        {

            await _unitOfWork.Sectors.Add(newSector);
            await _unitOfWork.CommitAsync();
            return newSector;
        }
        public async Task<List<Sector>> CreateRange(List<Sector> newSector)
        {

            await _unitOfWork.Sectors.AddRange(newSector);
            await _unitOfWork.CommitAsync();
            return newSector;
        }
        public async Task<IEnumerable<Sector>> GetAll()
        {
            return
                           await _unitOfWork.Sectors.GetAll();
        }

       /* public async Task Delete(Sector Sector)
        {
            _unitOfWork.Sectors.Remove(Sector);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Sector>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Sectors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Sector> GetById(int id)
        {
            return
                       await _unitOfWork.Sectors.SingleOrDefault(i => i.IdSector == id && i.Active == 0);
        }
   
        public async Task Update(Sector SectorToBeUpdated, Sector Sector)
        {
            SectorToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            Sector.Version = SectorToBeUpdated.Version + 1;
            Sector.IdSector = SectorToBeUpdated.IdSector;
            Sector.Status = Status.Pending;
            Sector.Active = 1;

            await _unitOfWork.Sectors.Add(Sector);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Sector Sector)
        {
            //Sector musi =  _unitOfWork.Sectors.SingleOrDefaultAsync(x=>x.Id == SectorToBeUpdated.Id);
            Sector.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Sector> Sector)
        {
            foreach (var item in Sector)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Sector>> GetAllActif()
        {
            return
                             await _unitOfWork.Sectors.GetAllActif();
        }

        public async Task<IEnumerable<Sector>> GetAllInActif()
        {
            return
                             await _unitOfWork.Sectors.GetAllInActif();
        }
        //public Task<Sector> CreateSector(Sector newSector)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSector(Sector Sector)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Sector> GetSectorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Sector>> GetSectorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSector(Sector SectorToBeUpdated, Sector Sector)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
