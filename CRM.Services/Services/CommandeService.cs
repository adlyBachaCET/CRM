using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CommandeService : ICommandeService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CommandeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Commande> Create(Commande newCommande)
        {

            await _unitOfWork.Commandes.Add(newCommande);
            await _unitOfWork.CommitAsync();
            return newCommande;
        }
        public async Task<List<Commande>> CreateRange(List<Commande> newCommande)
        {

            await _unitOfWork.Commandes.AddRange(newCommande);
            await _unitOfWork.CommitAsync();
            return newCommande;
        }
        public async Task<IEnumerable<Commande>> GetAll()
        {
            return
                           await _unitOfWork.Commandes.GetAll();
        }

       /* public async Task Delete(Commande Commande)
        {
            _unitOfWork.Commandes.Remove(Commande);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Commande>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Commandes
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Commande> GetById(int id)
        {
             return
               await _unitOfWork.Commandes.SingleOrDefault(i => i.IdCommande == id && i.Active == 0);
        }
   
        public async Task Update(Commande CommandeToBeUpdated, Commande Commande)
        {
            CommandeToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Commande.Version = CommandeToBeUpdated.Version + 1;
            Commande.IdCommande = CommandeToBeUpdated.IdCommande;
            Commande.Status = Status.Pending;
            Commande.Active = 0;

            await _unitOfWork.Commandes.Add(Commande);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Commande Commande)
        {
            //Commande musi =  _unitOfWork.Commandes.SingleOrDefaultAsync(x=>x.Id == CommandeToBeUpdated.Id);
             _unitOfWork.Commandes.Remove(Commande);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Commande> Commande)
        {
            foreach (var item in Commande)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Commande>> GetAllActif()
        {
            return
                             await _unitOfWork.Commandes.GetAllActif();
        }

        public async Task<IEnumerable<Commande>> GetAllInActif()
        {
            return
                             await _unitOfWork.Commandes.GetAllInActif();
        }

        public async Task<IEnumerable<Commande>> GetByIdDoctor(int id)
        {
            return
                            await _unitOfWork.Commandes.GetByIdDoctor(id);
        }

        public async Task<IEnumerable<Commande>> GetByIdActifDoctor(int Id)
        {
            return
                  await _unitOfWork.Commandes.GetByIdActifDoctor(Id);
        }
        public async Task<IEnumerable<Commande>> GetByIdActifUser(int Id)
        {
            return
                  await _unitOfWork.Commandes.GetByIdActifUser(Id);
        }
        //public Task<Commande> CreateCommande(Commande newCommande)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCommande(Commande Commande)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Commande> GetCommandeById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Commande>> GetCommandesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCommande(Commande CommandeToBeUpdated, Commande Commande)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
