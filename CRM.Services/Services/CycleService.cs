
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleService : ICycleService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cycle> Create(Cycle newCycle)
        {

            await _unitOfWork.Cycles.Add(newCycle);
            await _unitOfWork.CommitAsync();
            return newCycle;
        }
        public async Task<List<Cycle>> CreateRange(List<Cycle> newCycle)
        {

            await _unitOfWork.Cycles.AddRange(newCycle);
            await _unitOfWork.CommitAsync();
            return newCycle;
        }
        public async Task<IEnumerable<Cycle>> GetAll()
        {
            return
                           await _unitOfWork.Cycles.GetAll();
        }

       /* public async Task Delete(Cycle Cycle)
        {
            _unitOfWork.Cycles.Remove(Cycle);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Cycle>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Cycles
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Cycle> GetById(int id)
        {
            return
                   await _unitOfWork.Cycles.GetByIdActif(id);
        }
   
        public async Task Update(Cycle CycleToBeUpdated, Cycle Cycle)
        {
          CycleToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

          Cycle.Version = CycleToBeUpdated.Version + 1;
          Cycle.IdCycle = CycleToBeUpdated.IdCycle;
          Cycle.Status = Status.Approuved;
          Cycle.Active = 0;

            await _unitOfWork.Cycles.Add(Cycle);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Cycle Cycle)
        {
            //Cycle musi =  _unitOfWork.Cycles.SingleOrDefaultAsync(x=>x.Id == CycleToBeUpdated.Id);
            Cycle.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Cycle> Cycle)
        {
            foreach (var item in Cycle)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Cycle>> GetAllActif()
        {
            return
                             await _unitOfWork.Cycles.GetAllActif();
        }

        public async Task<IEnumerable<Cycle>> GetAllInActif()
        {
            return
                             await _unitOfWork.Cycles.GetAllInActif();
        }
        //public Task<Cycle> CreateCycle(Cycle newCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCycle(Cycle Cycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Cycle> GetCycleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Cycle>> GetCyclesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCycle(Cycle CycleToBeUpdated, Cycle Cycle)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
