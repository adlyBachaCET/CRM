using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Specialty> Create(Specialty newSpecialty)
        {

            await _unitOfWork.Specialtys.Add(newSpecialty);
            await _unitOfWork.CommitAsync();
            return newSpecialty;
        }
        public async Task<List<Specialty>> CreateRange(List<Specialty> newSpecialty)
        {

            await _unitOfWork.Specialtys.AddRange(newSpecialty);
            await _unitOfWork.CommitAsync();
            return newSpecialty;
        }
        public async Task<IEnumerable<Specialty>> GetAll()
        {
            return
                           await _unitOfWork.Specialtys.GetAll();
        }
      
        /* public async Task Delete(Specialty Specialty)
         {
             _unitOfWork.Specialtys.Remove(Specialty);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Specialty>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Specialtys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Specialty> GetById(int id)
        {
            return
                 await _unitOfWork.Specialtys.SingleOrDefault(i => i.IdSpecialty == id && i.Active == 0);
        }
   
        public async Task Update(Specialty SpecialtyToBeUpdated, Specialty Specialty)
        {
            SpecialtyToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Specialty.Version = SpecialtyToBeUpdated.Version + 1;
            Specialty.IdSpecialty = SpecialtyToBeUpdated.IdSpecialty;
            Specialty.Status = Status.Pending;
            Specialty.Active = 0;

            await _unitOfWork.Specialtys.Add(Specialty);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Specialty Specialty)
        {
            //Specialty musi =  _unitOfWork.Specialtys.SingleOrDefaultAsync(x=>x.Id == SpecialtyToBeUpdated.Id);
            Specialty.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Specialty> Specialty)
        {
            foreach (var item in Specialty)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Specialty>> GetAllActif()
        {
            return
                             await _unitOfWork.Specialtys.GetAllActif();
        }

        public async Task<IEnumerable<Specialty>> GetAllInActif()
        {
            return
                             await _unitOfWork.Specialtys.GetAllInActif();
        }
        //public Task<Specialty> CreateSpecialty(Specialty newSpecialty)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSpecialty(Specialty Specialty)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Specialty> GetSpecialtyById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Specialty>> GetSpecialtysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSpecialty(Specialty SpecialtyToBeUpdated, Specialty Specialty)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
