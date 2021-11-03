using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleSectorWeekDoctorsService : ICycleSectorWeekDoctorsService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleSectorWeekDoctorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CycleSectorWeekDoctors> Create(CycleSectorWeekDoctors newCycleSectorWeekDoctors)
        {

            await _unitOfWork.CycleSectorWeekDoctors.Add(newCycleSectorWeekDoctors);
            await _unitOfWork.CommitAsync();
            return newCycleSectorWeekDoctors;
        }
        public async Task<List<CycleSectorWeekDoctors>> CreateRange(List<CycleSectorWeekDoctors> newCycleSectorWeekDoctors)
        {

            await _unitOfWork.CycleSectorWeekDoctors.AddRange(newCycleSectorWeekDoctors);
            await _unitOfWork.CommitAsync();
            return newCycleSectorWeekDoctors;
        }
        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAll()
        {
            return
                           await _unitOfWork.CycleSectorWeekDoctors.GetAll();
        }
        public async Task<Doctor> GetDoctor(int IdDoctor)
        {
            var a=await _unitOfWork.CycleSectorWeekDoctors.SingleOrDefault(i=>i.IdDoctor==IdDoctor);
            return a.IdDoctorNavigation;
        }
      

        public async Task<CycleSectorWeekDoctors> GetById(int id)
        {
            return
                await _unitOfWork.CycleSectorWeekDoctors.GetById(id);
        }
   
        public async Task Update(CycleSectorWeekDoctors CycleSectorWeekDoctorsToBeUpdated, CycleSectorWeekDoctors CycleSectorWeekDoctors)
        {
            CycleSectorWeekDoctors.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(CycleSectorWeekDoctors CycleSectorWeekDoctors)
        {
            //CycleSectorWeekDoctors musi =  _unitOfWork.CycleSectorWeekDoctors.SingleOrDefaultAsync(x=>x.Id == CycleSectorWeekDoctorsToBeUpdated.Id);
            CycleSectorWeekDoctors.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<CycleSectorWeekDoctors> CycleSectorWeekDoctors)
        {
            foreach (var item in CycleSectorWeekDoctors)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllActif()
        {
            return
                             await _unitOfWork.CycleSectorWeekDoctors.GetAllActif();
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllInActif()
        {
            return
                             await _unitOfWork.CycleSectorWeekDoctors.GetAllInActif();
        }
        //public Task<CycleSectorWeekDoctors> CreateCycleSectorWeekDoctors(CycleSectorWeekDoctors newCycleSectorWeekDoctors)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCycleSectorWeekDoctors(CycleSectorWeekDoctors CycleSectorWeekDoctors)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<CycleSectorWeekDoctors> GetCycleSectorWeekDoctorsById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<CycleSectorWeekDoctors>> GetCycleSectorWeekDoctorssByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCycleSectorWeekDoctors(CycleSectorWeekDoctors CycleSectorWeekDoctorsToBeUpdated, CycleSectorWeekDoctors CycleSectorWeekDoctors)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
