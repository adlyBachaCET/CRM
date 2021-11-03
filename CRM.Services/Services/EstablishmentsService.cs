
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class EstablishmentsService : IEstablishmentService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public EstablishmentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Establishment> Create(Establishment newEstablishment)
        {

            await _unitOfWork.Establishments.Add(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }
        public async Task<List<Establishment>> CreateRange(List<Establishment> newEstablishment)
        {

            await _unitOfWork.Establishments.AddRange(newEstablishment);
            await _unitOfWork.CommitAsync();
            return newEstablishment;
        }
        public async Task<IEnumerable<Establishment>> GetAll()
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

        public async Task<Establishment> GetById(int id)
        {
            return
               await _unitOfWork.Establishments.SingleOrDefault(i => i.IdEstablishment == id &&i.Active == 0);
        }
   
        public async Task Update(Establishment EstablishmentToBeUpdated, Establishment Establishment)
        {
           EstablishmentToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

           Establishment.Version =EstablishmentToBeUpdated.Version + 1;
           Establishment.IdEstablishment =EstablishmentToBeUpdated.IdEstablishment;
           Establishment.Status = Status.Pending;
           Establishment.Active = 1;

            await _unitOfWork.Establishments.Add(Establishment);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Establishment Establishment)
        {
            //Establishment musi =  _unitOfWork.Establishments.SingleOrDefaultAsync(x=>x.Id == EstablishmentToBeUpdated.Id);
            Establishment.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Establishment> Establishment)
        {
            foreach (var item in Establishment)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Establishment>> GetAllActif()
        {
            return
                             await _unitOfWork.Establishments.GetAllActif();
        }

        public async Task<IEnumerable<Establishment>> GetAllInActif()
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
