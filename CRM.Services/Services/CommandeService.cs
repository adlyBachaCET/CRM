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

 

        public async Task<Commande> GetById(int id)
        {
             return
               await _unitOfWork.Commandes.GetByIdActif(id);
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


        public async Task<IEnumerable<Commande>> GetByIdActifUser(int Id)
        {
            return
                  await _unitOfWork.Commandes.GetByIdActifUser(Id);
        }

        public async Task<IEnumerable<Commande>> GetByIdActifPharmacy(int Id)
        {
            return
         await _unitOfWork.Commandes.GetByIdActifPharmacy(Id);
        }
      
    }
}
