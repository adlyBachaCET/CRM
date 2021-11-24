using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PotentielCycleService : IPotentielCycleService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PotentielCycleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PotentielCycle> Create(PotentielCycle newPotentielCycle)
        {

            await _unitOfWork.PotentielCycles.Add(newPotentielCycle);
            await _unitOfWork.CommitAsync();
            return newPotentielCycle;
        }
        public async Task<List<PotentielCycle>> CreateRange(List<PotentielCycle> newPotentielCycle)
        {

            await _unitOfWork.PotentielCycles.AddRange(newPotentielCycle);
            await _unitOfWork.CommitAsync();
            return newPotentielCycle;
        }
        public async Task<IEnumerable<PotentielCycle>> GetAll()
        {
            return
                           await _unitOfWork.PotentielCycles.GetAll();
        }

       /* public async Task Delete(PotentielCycle PotentielCycle)
        {
            _unitOfWork.PotentielCycles.Remove(PotentielCycle);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<PotentielCycle>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.PotentielCycles
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<PotentielCycle> GetById(int id)
        {
            return
                await _unitOfWork.PotentielCycles.GetById(id);
        }
        public async Task<IEnumerable<Potentiel>> GetPotentielsById(int id)
        {
            List<Potentiel> Potentiels = new List<Potentiel>();
            var PotentielCycles = await _unitOfWork.PotentielCycles.Find(i => i.IdCycle == id);
            foreach(var item in PotentielCycles)
            {
                var Potentiel = await _unitOfWork.Potentiels.SingleOrDefault(i => i.IdPotentiel == item.IdPotentiel);
                Potentiels.Add(Potentiel);
            }
            return Potentiels;
               ;
        }
        public async Task Update(PotentielCycle PotentielCycleToBeUpdated, PotentielCycle PotentielCycle)
        {
            PotentielCycle.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(PotentielCycle PotentielCycle)
        {
            //PotentielCycle musi =  _unitOfWork.PotentielCycles.SingleOrDefaultAsync(x=>x.Id == PotentielCycleToBeUpdated.Id);
            PotentielCycle.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<PotentielCycle> PotentielCycle)
        {
            foreach (var item in PotentielCycle)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllActif()
        {
            return
                             await _unitOfWork.PotentielCycles.GetAllActif();
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllInActif()
        {
            return
                             await _unitOfWork.PotentielCycles.GetAllInActif();
        }

        public async Task<PotentielCycle> GetByIdPotentielCycle(int IdPotentiel, int idCycle)
        {
            return
                           await _unitOfWork.PotentielCycles.SingleOrDefault(i=>i.IdCycle==idCycle&& i.IdPotentiel==IdPotentiel&& i.Active==0);
        }
        //public Task<PotentielCycle> CreatePotentielCycle(PotentielCycle newPotentielCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePotentielCycle(PotentielCycle PotentielCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<PotentielCycle> GetPotentielCycleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<PotentielCycle>> GetPotentielCyclesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePotentielCycle(PotentielCycle PotentielCycleToBeUpdated, PotentielCycle PotentielCycle)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
