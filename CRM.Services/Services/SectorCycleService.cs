using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SectorCycleService : ISectorCycleService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SectorCycleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SectorCycle> Create(SectorCycle newWeekSectorCycle)
        {

            await _unitOfWork.WeekSectorCycles.Add(newWeekSectorCycle);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycle;
        }
        public async Task<List<SectorCycle>> CreateRange(List<SectorCycle> newWeekSectorCycle)
        {

            await _unitOfWork.WeekSectorCycles.AddRange(newWeekSectorCycle);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycle;
        }
        public async Task<IEnumerable<SectorCycle>> GetAll()
        {
            return
                           await _unitOfWork.WeekSectorCycles.GetAll();
        }

       /* public async Task Delete(WeekSectorCycle WeekSectorCycle)
        {
            _unitOfWork.WeekSectorCycles.Remove(WeekSectorCycle);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WeekSectorCycle>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WeekSectorCycles
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<SectorCycle> GetById(int id)
        {
            return
                await _unitOfWork.WeekSectorCycles.GetById(id);
        }
   
        public async Task Update(SectorCycle WeekSectorCycleToBeUpdated, SectorCycle WeekSectorCycle)
        {
            WeekSectorCycle.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SectorCycle WeekSectorCycle)
        {
            //WeekSectorCycle musi =  _unitOfWork.WeekSectorCycles.SingleOrDefaultAsync(x=>x.Id == WeekSectorCycleToBeUpdated.Id);
            WeekSectorCycle.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SectorCycle> WeekSectorCycle)
        {
            foreach (var item in WeekSectorCycle)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SectorCycle>> GetAllActif()
        {
            return
                             await _unitOfWork.WeekSectorCycles.GetAllActif();
        }

        public async Task<IEnumerable<SectorCycle>> GetAllInActif()
        {
            return
                             await _unitOfWork.WeekSectorCycles.GetAllInActif();
        }
        //public Task<WeekSectorCycle> CreateWeekSectorCycle(WeekSectorCycle newWeekSectorCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWeekSectorCycle(WeekSectorCycle WeekSectorCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WeekSectorCycle> GetWeekSectorCycleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WeekSectorCycle>> GetWeekSectorCyclesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWeekSectorCycle(WeekSectorCycle WeekSectorCycleToBeUpdated, WeekSectorCycle WeekSectorCycle)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
