using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class WeekInYearService : IWeekInYearService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public WeekInYearService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekInYear> Create(WeekInYear newWeekInYear)
        {

            await _unitOfWork.WeekInYears.Add(newWeekInYear);
            await _unitOfWork.CommitAsync();
            return newWeekInYear;
        }
        public async Task<List<WeekInYear>> CreateRange(List<WeekInYear> newWeekInYear)
        {

            await _unitOfWork.WeekInYears.AddRange(newWeekInYear);
            await _unitOfWork.CommitAsync();
            return newWeekInYear;
        }
        public async Task<IEnumerable<WeekInYear>> GetAll()
        {
            return
                           await _unitOfWork.WeekInYears.GetAll();
        }

       /* public async Task Delete(WeekInYear WeekInYear)
        {
            _unitOfWork.WeekInYears.Remove(WeekInYear);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<WeekInYear>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.WeekInYears
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<WeekInYear> GetById(int ordre,int year)
        {
            return
                  await _unitOfWork.WeekInYears.SingleOrDefault(i => i.Order == ordre && i.Year==year && i.Active == 0);
        }
   
        public async Task Update(WeekInYear WeekInYearToBeUpdated, WeekInYear WeekInYear)
        {
            WeekInYearToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            WeekInYear.Version = WeekInYearToBeUpdated.Version + 1;
            WeekInYear.Order = WeekInYearToBeUpdated.Order;
            WeekInYear.Year = WeekInYearToBeUpdated.Year;

            WeekInYear.Status = Status.Pending;
            WeekInYear.Active = 1;

            await _unitOfWork.WeekInYears.Add(WeekInYear);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(WeekInYear WeekInYear)
        {
            //WeekInYear musi =  _unitOfWork.WeekInYears.SingleOrDefaultAsync(x=>x.Id == WeekInYearToBeUpdated.Id);
            WeekInYear.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<WeekInYear> WeekInYear)
        {
            foreach (var item in WeekInYear)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<WeekInYear>> GetAllActif()
        {
            return
                             await _unitOfWork.WeekInYears.GetAllActif();
        }

        public async Task<IEnumerable<WeekInYear>> GetAllInActif()
        {
            return
                             await _unitOfWork.WeekInYears.GetAllInActif();
        }
        //public Task<WeekInYear> CreateWeekInYear(WeekInYear newWeekInYear)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteWeekInYear(WeekInYear WeekInYear)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WeekInYear> GetWeekInYearById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<WeekInYear>> GetWeekInYearsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateWeekInYear(WeekInYear WeekInYearToBeUpdated, WeekInYear WeekInYear)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
