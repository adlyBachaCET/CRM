using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class WeekSectorCycleInYearService : IWeekSectorCycleInYearService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public WeekSectorCycleInYearService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekSectorCycleInYear> Create(WeekSectorCycleInYear newWeekSectorCycleInYear)
        {

            await _unitOfWork.WeekSectorCycleInYears.Add(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<List<WeekSectorCycleInYear>> CreateRange(List<WeekSectorCycleInYear> newWeekSectorCycleInYear)
        {

            await _unitOfWork.WeekSectorCycleInYears.AddRange(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAll()
        {
            return
                           await _unitOfWork.WeekSectorCycleInYears.GetAll();
        }

       /* public async Task Delete(WeekSectorCycleInYear WeekSectorCycleInYear)
        {
            _unitOfWork.WeekSectorCycleInYears.Remove(WeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WeekSectorCycleInYears
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<WeekSectorCycleInYear> GetById(int id)
        {
            return
                await _unitOfWork.WeekSectorCycleInYears.GetById(id);
        }
   
        public async Task Update(WeekSectorCycleInYear WeekSectorCycleInYearToBeUpdated, WeekSectorCycleInYear WeekSectorCycleInYear)
        {
            WeekSectorCycleInYear.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(WeekSectorCycleInYear WeekSectorCycleInYear)
        {
            //WeekSectorCycleInYear musi =  _unitOfWork.WeekSectorCycleInYears.SingleOrDefaultAsync(x=>x.Id == WeekSectorCycleInYearToBeUpdated.Id);
            WeekSectorCycleInYear.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<WeekSectorCycleInYear> WeekSectorCycleInYear)
        {
            foreach (var item in WeekSectorCycleInYear)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllActif()
        {
            return
                             await _unitOfWork.WeekSectorCycleInYears.GetAllActif();
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllInActif()
        {
            return
                             await _unitOfWork.WeekSectorCycleInYears.GetAllInActif();
        }
        //public Task<WeekSectorCycleInYear> CreateWeekSectorCycleInYear(WeekSectorCycleInYear newWeekSectorCycleInYear)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWeekSectorCycleInYear(WeekSectorCycleInYear WeekSectorCycleInYear)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WeekSectorCycleInYear> GetWeekSectorCycleInYearById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WeekSectorCycleInYear>> GetWeekSectorCycleInYearsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWeekSectorCycleInYear(WeekSectorCycleInYear WeekSectorCycleInYearToBeUpdated, WeekSectorCycleInYear WeekSectorCycleInYear)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
