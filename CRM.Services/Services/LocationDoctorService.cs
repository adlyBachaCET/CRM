using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class LocationDoctorService : ILocationDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public LocationDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LocationDoctor> Create(LocationDoctor newEstablishmentDoctor)
        {

            await _unitOfWork.LocationDoctors.Add(newEstablishmentDoctor);
            await _unitOfWork.CommitAsync();
            return newEstablishmentDoctor;
        }
        public async Task<List<LocationDoctor>> CreateRange(List<LocationDoctor> newEstablishmentDoctor)
        {

            await _unitOfWork.LocationDoctors.AddRange(newEstablishmentDoctor);
            await _unitOfWork.CommitAsync();
            return newEstablishmentDoctor;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAll()
        {
            return
                           await _unitOfWork.LocationDoctors.GetAll();
        }

       /* public async Task Delete(EstablishmentDoctor EstablishmentDoctor)
        {
            _unitOfWork.EstablishmentDoctors.Remove(EstablishmentDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<EstablishmentDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.EstablishmentDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<LocationDoctor> GetByIdActif(int id,int IdLocation)
        {
            return
                await _unitOfWork.LocationDoctors.GetByIdActif(id, IdLocation);
        }
   
        public async Task Update(LocationDoctor EstablishmentDoctorToBeUpdated, LocationDoctor EstablishmentDoctor)
        {
            EstablishmentDoctor.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(LocationDoctor EstablishmentDoctor)
        {
            //EstablishmentDoctor musi =  _unitOfWork.EstablishmentDoctors.SingleOrDefaultAsync(x=>x.Id == EstablishmentDoctorToBeUpdated.Id);
            EstablishmentDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<LocationDoctor> EstablishmentDoctor)
        {
            foreach (var item in EstablishmentDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.LocationDoctors.GetAllActif();
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.LocationDoctors.GetAllInActif();
        }

        public async  Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif(int Id)
        {
            return
                         await _unitOfWork.LocationDoctors.GetAllAcceptedActif(Id);
        }

        public async Task<LocationDoctor> GetById(int id1, int Id2)
        {
            return
                                    await _unitOfWork.LocationDoctors.SingleOrDefault(i=>i.IdDoctor==id1&&i.IdLocation==Id2);
        }
        //public Task<EstablishmentDoctor> CreateEstablishmentDoctor(EstablishmentDoctor newEstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishmentDoctor(EstablishmentDoctor EstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EstablishmentDoctor> GetEstablishmentDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EstablishmentDoctor>> GetEstablishmentDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishmentDoctor(EstablishmentDoctor EstablishmentDoctorToBeUpdated, EstablishmentDoctor EstablishmentDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
