
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

            await _unitOfWork.Establishments.Add(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }
        public async Task<List<Location>> CreateRange(List<Location> newEstablishment)
        {

            await _unitOfWork.Establishments.AddRange(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }
        public async Task<IEnumerable<Location>> GetAll()
        {
            return
                           await _unitOfWork.Establishments.GetAll();
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

        public async Task<Location> GetById(int id)
        {
            return
               await _unitOfWork.Establishments.SingleOrDefault(i => i.IdLocation== id &&i.Active == 0);
        }
   
        public async Task Update(Location EstablishmentToBeUpdated, Location Establishment)
        {
           EstablishmentToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

           Establishment.Version =EstablishmentToBeUpdated.Version + 1;
           Establishment.IdLocation = EstablishmentToBeUpdated.IdLocation;
           Establishment.Status = Status.Pending;
           Establishment.Active =0;

            await _unitOfWork.Establishments.Add(Establishment);
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
                             await _unitOfWork.Establishments.GetAllActif();
        }

        public async Task<IEnumerable<Location>> GetAllInActif()
        {
            return
                             await _unitOfWork.Establishments.GetAllInActif();
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
