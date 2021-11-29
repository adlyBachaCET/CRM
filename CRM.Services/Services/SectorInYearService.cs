using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SectorInYearService : ISectorInYearService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SectorInYearService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SectorInYear> Create(SectorInYear newWeekSectorCycleInYear)
        {

            await _unitOfWork.SectorInYear.Add(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<List<SectorInYear>> CreateRange(List<SectorInYear> newWeekSectorCycleInYear)
        {

            await _unitOfWork.SectorInYear.AddRange(newWeekSectorCycleInYear);
            await _unitOfWork.CommitAsync();
            return newWeekSectorCycleInYear;
        }
        public async Task<IEnumerable<SectorInYear>> GetAll()
        {
            return
                           await _unitOfWork.SectorInYear.GetAll();
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

        public async Task<SectorInYear> GetById(int id)
        {
            return
                await _unitOfWork.SectorInYear.GetById(id);
        }
   
        public async Task Update(SectorInYear WeekSectorCycleInYearToBeUpdated, SectorInYear WeekSectorCycleInYear)
        {
            WeekSectorCycleInYear.Active = 1;
            await _unitOfWork.CommitAsync();
        }
        public async Task RequestOpeningWeek(int IdSector)
        {
            var SectorInYear=await _unitOfWork.SectorInYear.SingleOrDefault(i=>i.IdSector==IdSector&& i.Active==0);

            SectorInYear.Request =true ;
            await _unitOfWork.CommitAsync();
        }
        public async Task DenyRequestOpeningWeek(int IdSector)
        {
            var SectorInYear = await _unitOfWork.SectorInYear.SingleOrDefault(i => i.IdSector == IdSector && i.Active == 0);
            SectorInYear.Lock = 1;
            SectorInYear.Request = false;
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(SectorInYear WeekSectorCycleInYear)
        {
            //WeekSectorCycleInYear musi =  _unitOfWork.WeekSectorCycleInYears.SingleOrDefaultAsync(x=>x.Id == WeekSectorCycleInYearToBeUpdated.Id);
            WeekSectorCycleInYear.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SectorInYear> WeekSectorCycleInYear)
        {
            foreach (var item in WeekSectorCycleInYear)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SectorInYear>> GetAllActif()
        {
            return
                             await _unitOfWork.SectorInYear.GetAllActif();
        }

        public async Task<IEnumerable<SectorInYear>> GetAllInActif()
        {
            return
                             await _unitOfWork.SectorInYear.GetAllInActif();
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
