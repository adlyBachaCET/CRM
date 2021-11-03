using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class WeekInCycleService : IWeekInCycleService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public WeekInCycleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekInCycle> Create(WeekInCycle newWeekInCycle)
        {

            await _unitOfWork.WeekInCycles.Add(newWeekInCycle);
            await _unitOfWork.CommitAsync();
            return newWeekInCycle;
        }
        public async Task<List<WeekInCycle>> CreateRange(List<WeekInCycle> newWeekInCycle)
        {

            await _unitOfWork.WeekInCycles.AddRange(newWeekInCycle);
            await _unitOfWork.CommitAsync();
            return newWeekInCycle;
        }
        public async Task<IEnumerable<WeekInCycle>> GetAll()
        {
            return
                           await _unitOfWork.WeekInCycles.GetAll();
        }

       /* public async Task Delete(WeekInCycle WeekInCycle)
        {
            _unitOfWork.WeekInCycles.Remove(WeekInCycle);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WeekInCycle>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WeekInCycles
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<WeekInCycle> GetById(int id)
        {
            return
                  await _unitOfWork.WeekInCycles.SingleOrDefault(i => i.IdWeekCycle == id && i.Active == 0);
        }
   
        public async Task Update(WeekInCycle WeekInCycleToBeUpdated, WeekInCycle WeekInCycle)
        {
            WeekInCycleToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            WeekInCycle.Version = WeekInCycleToBeUpdated.Version + 1;
            WeekInCycle.IdWeekCycle = WeekInCycleToBeUpdated.IdWeekCycle;
            WeekInCycle.Status = Status.Pending;
            WeekInCycle.Active = 1;

            await _unitOfWork.WeekInCycles.Add(WeekInCycle);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(WeekInCycle WeekInCycle)
        {
            //WeekInCycle musi =  _unitOfWork.WeekInCycles.SingleOrDefaultAsync(x=>x.Id == WeekInCycleToBeUpdated.Id);
            WeekInCycle.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<WeekInCycle> WeekInCycle)
        {
            foreach (var item in WeekInCycle)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllActif()
        {
            return
                             await _unitOfWork.WeekInCycles.GetAllActif();
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllInActif()
        {
            return
                             await _unitOfWork.WeekInCycles.GetAllInActif();
        }
        //public Task<WeekInCycle> CreateWeekInCycle(WeekInCycle newWeekInCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWeekInCycle(WeekInCycle WeekInCycle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WeekInCycle> GetWeekInCycleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WeekInCycle>> GetWeekInCyclesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWeekInCycle(WeekInCycle WeekInCycleToBeUpdated, WeekInCycle WeekInCycle)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
