using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class PotentielService : IPotentielService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public PotentielService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Potentiel> Create(Potentiel newPotentiel)
        {

            await _unitOfWork.Potentiels.Add(newPotentiel);
            await _unitOfWork.CommitAsync();
            return newPotentiel;
        }
        public async Task<List<Potentiel>> CreateRange(List<Potentiel> newPotentiel)
        {

            await _unitOfWork.Potentiels.AddRange(newPotentiel);
            await _unitOfWork.CommitAsync();
            return newPotentiel;
        }
        public async Task<IEnumerable<Potentiel>> GetAll()
        {
            return
                           await _unitOfWork.Potentiels.GetAll();
        }

       /* public async Task Delete(Potentiel Potentiel)
        {
            _unitOfWork.Potentiels.Remove(Potentiel);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Potentiel>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Potentiels
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Potentiel> GetById(int id)
        {
            return
                    await _unitOfWork.Potentiels.SingleOrDefault(i => i.IdPotentiel == id && i.Active == 0);
        }
   
        public async Task Update(Potentiel PotentielToBeUpdated, Potentiel Potentiel)
        {
            PotentielToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            Potentiel.Version = PotentielToBeUpdated.Version + 1;
            Potentiel.IdPotentiel = PotentielToBeUpdated.IdPotentiel;
            Potentiel.Status = Status.Pending;
            Potentiel.Active = 1;

            await _unitOfWork.Potentiels.Add(Potentiel);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Potentiel Potentiel)
        {
            //Potentiel musi =  _unitOfWork.Potentiels.SingleOrDefaultAsync(x=>x.Id == PotentielToBeUpdated.Id);
            Potentiel.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Potentiel> Potentiel)
        {
            foreach (var item in Potentiel)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Potentiel>> GetAllActif()
        {
            return
                             await _unitOfWork.Potentiels.GetAllActif();
        }

        public async Task<IEnumerable<Potentiel>> GetAllInActif()
        {
            return
                             await _unitOfWork.Potentiels.GetAllInActif();
        }
        //public Task<Potentiel> CreatePotentiel(Potentiel newPotentiel)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeletePotentiel(Potentiel Potentiel)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Potentiel> GetPotentielById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Potentiel>> GetPotentielsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdatePotentiel(Potentiel PotentielToBeUpdated, Potentiel Potentiel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
