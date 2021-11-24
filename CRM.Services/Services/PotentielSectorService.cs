using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PotentielSectorService : IPotentielSectorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PotentielSectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PotentielSector> Create(PotentielSector newPotentielSector)
        {

            await _unitOfWork.PotentielSectors.Add(newPotentielSector);
            await _unitOfWork.CommitAsync();
            return newPotentielSector;
        }
        public async Task<List<PotentielSector>> CreateRange(List<PotentielSector> newPotentielSector)
        {

            await _unitOfWork.PotentielSectors.AddRange(newPotentielSector);
            await _unitOfWork.CommitAsync();
            return newPotentielSector;
        }
        public async Task<IEnumerable<PotentielSector>> GetAll()
        {
            return
                           await _unitOfWork.PotentielSectors.GetAll();
        }

       /* public async Task Delete(PotentielSector PotentielSector)
        {
            _unitOfWork.PotentielSectors.Remove(PotentielSector);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<PotentielSector>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.PotentielSectors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<PotentielSector> GetById(int id)
        {
            return
                await _unitOfWork.PotentielSectors.GetById(id);
        }
        public async Task<IEnumerable<Potentiel>> GetPotentielsById(int id)
        {
            List<Potentiel> Potentiels = new List<Potentiel>();
            var PotentielSectors = await _unitOfWork.PotentielSectors.Find(i => i.IdSector == id);
            foreach(var item in PotentielSectors)
            {
                var Potentiel = await _unitOfWork.Potentiels.SingleOrDefault(i => i.IdPotentiel == item.IdPotentiel);
                Potentiels.Add(Potentiel);
            }
            return Potentiels;
               ;
        }
        public async Task Update(PotentielSector PotentielSectorToBeUpdated, PotentielSector PotentielSector)
        {
            PotentielSector.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(PotentielSector PotentielSector)
        {
            //PotentielSector musi =  _unitOfWork.PotentielSectors.SingleOrDefaultAsync(x=>x.Id == PotentielSectorToBeUpdated.Id);
            PotentielSector.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<PotentielSector> PotentielSector)
        {
            foreach (var item in PotentielSector)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<PotentielSector>> GetAllActif()
        {
            return
                             await _unitOfWork.PotentielSectors.GetAllActif();
        }

        public async Task<IEnumerable<PotentielSector>> GetAllInActif()
        {
            return
                             await _unitOfWork.PotentielSectors.GetAllInActif();
        }

        public async Task<PotentielSector> GetByIdPotentielSector(int IdPotentiel, int idSector)
        {
            return
                           await _unitOfWork.PotentielSectors.SingleOrDefault(i=>i.IdSector==idSector&& i.IdPotentiel==IdPotentiel&& i.Active==0);
        }
        //public Task<PotentielSector> CreatePotentielSector(PotentielSector newPotentielSector)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePotentielSector(PotentielSector PotentielSector)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<PotentielSector> GetPotentielSectorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<PotentielSector>> GetPotentielSectorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePotentielSector(PotentielSector PotentielSectorToBeUpdated, PotentielSector PotentielSector)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
