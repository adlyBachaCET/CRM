using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ICommandeService
    {
        Task<Commande> GetById(int id);
        Task<Commande> Create(Commande newCommande);
        Task<List<Commande>> CreateRange(List<Commande> newCommande);
        Task Update(Commande CommandeToBeUpdated, Commande Commande);
        Task Delete(Commande CommandeToBeDeleted);
        Task DeleteRange(List<Commande> Commande);
        Task<IEnumerable<Commande>> GetByIdActifPharmacy(int Id);

        Task<IEnumerable<Commande>> GetAll();
        Task<IEnumerable<Commande>> GetAllActif();
        Task<IEnumerable<Commande>> GetAllInActif();
        Task<IEnumerable<Commande>> GetByIdDoctor(int id);
        Task<IEnumerable<Commande>> GetByIdActifDoctor(int Id);
        Task<IEnumerable<Commande>> GetByIdActifUser(int Id);

    }
}
