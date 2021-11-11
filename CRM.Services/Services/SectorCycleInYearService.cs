using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SectorCycleInYearService : ISectorCycleInYearService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SectorCycleInYearService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SectorCycleInYear> Create(SectorCycleInYear newWeekSectorCycleInYear)
        {

            await _unitOfWork.WeekSectorCycleInYears.Add(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<List<SectorCycleInYear>> CreateRange(List<SectorCycleInYear> newWeekSectorCycleInYear)
        {

            await _unitOfWork.WeekSectorCycleInYears.AddRange(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<IEnumerable<SectorCycleInYear>> GetAll()
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

        public async Task<SectorCycleInYear> GetById(int id)
        {
            return
                await _unitOfWork.WeekSectorCycleInYears.GetById(id);
        }
   
        public async Task Update(SectorCycleInYear WeekSectorCycleInYearToBeUpdated, SectorCycleInYear WeekSectorCycleInYear)
        {
            WeekSectorCycleInYear.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SectorCycleInYear WeekSectorCycleInYear)
        {
            //WeekSectorCycleInYear musi =  _unitOfWork.WeekSectorCycleInYears.SingleOrDefaultAsync(x=>x.Id == WeekSectorCycleInYearToBeUpdated.Id);
            WeekSectorCycleInYear.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SectorCycleInYear> WeekSectorCycleInYear)
        {
            foreach (var item in WeekSectorCycleInYear)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllActif()
        {
            return
                             await _unitOfWork.WeekSectorCycleInYears.GetAllActif();
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllInActif()
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
