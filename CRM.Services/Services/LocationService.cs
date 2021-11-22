
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class LocationService : ILocationService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public LocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Location> Create(Location newEstablishment)
        {

            await _unitOfWork.Locations.Add(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }

        public async Task<IEnumerable<Location>> GetByNearByActif(string Locality1, string Locality2, string Locality3, int CodePostal)
        {
            return
                       await _unitOfWork.Locations.GetByNearByActif(Locality1, Locality2, CodePostal);
        }
        public async Task<List<Location>> CreateRange(List<Location> newEstablishment)
        {

            await _unitOfWork.Locations.AddRange(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }
        public async Task<IEnumerable<Location>> GetAll()
        {
            return
                           await _unitOfWork.Locations.GetAll();
        }

        /* public async Task Delete(Establishment Establishment)
         {
             _unitOfWork.Establishments.Remove(Establishment);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Establishment>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Establishments
        //          .GetAllWithArtisteAsync();
        //}
        public async Task Approuve(Location LocationToBeUpdated, Location Location)
        {
            LocationToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Location = LocationToBeUpdated;
            Location.Version = LocationToBeUpdated.Version + 1;
            Location.IdLocation = LocationToBeUpdated.IdLocation;
            Location.Status = Status.Rejected;
            Location.UpdatedOn = System.DateTime.UtcNow;
            Location.CreatedOn = LocationToBeUpdated.CreatedOn;

            Location.Active = 0;

            await _unitOfWork.Locations.Add(Location);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Location LocationToBeUpdated, Location Location)
        {
            LocationToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Location.Version = LocationToBeUpdated.Version + 1;
            Location.IdLocation = LocationToBeUpdated.IdLocation;
            Location.Status = Status.Rejected;
            Location.Active = 1;

            await _unitOfWork.Locations.Add(Location);
            await _unitOfWork.CommitAsync();
        }
        public async Task<Location> GetById(int id)
        {
            return
               await _unitOfWork.Locations.GetByIdActif(id);
        }
   
        public async Task Update(Location EstablishmentToBeUpdated, Location Establishment)
        {
           EstablishmentToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

           Establishment.Version =EstablishmentToBeUpdated.Version + 1;
           Establishment.IdLocation = EstablishmentToBeUpdated.IdLocation;
           Establishment.Status = Status.Pending;
           Establishment.Active =0;

            await _unitOfWork.Locations.Add(Establishment);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Location Establishment)
        {
            //Establishment musi =  _unitOfWork.Establishments.SingleOrDefaultAsync(x=>x.Id == EstablishmentToBeUpdated.Id);
            Establishment.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Location> Establishment)
        {
            foreach (var item in Establishment)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Location>> GetAllActif()
        {
            return
                             await _unitOfWork.Locations.GetAllActif();
        }

        public async Task<IEnumerable<Location>> GetAllInActif()
        {
            return
                             await _unitOfWork.Locations.GetAllInActif();
        }

        public async Task<Location> GetByExistantActif(string Name, int? IdlocationType)
        {
            {
                return
                                 await _unitOfWork.Locations.GetByExistantActif(Name, IdlocationType);
            }
        }

        public async Task<Location> GetByExactExistantActif(string Name, int PostalCode, string Locality1, string Locality2)
        {
            return
                            await _unitOfWork.Locations.GetByExactExistantActif(Name, PostalCode, Locality1, Locality2);
        }
        //public Task<Establishment> CreateEstablishment(Establishment newEstablishment)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEstablishment(Establishment Establishment)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Establishment> GetEstablishmentById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Establishment>> GetEstablishmentsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateEstablishment(Establishment EstablishmentToBeUpdated, Establishment Establishment)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
