using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class CommandeRepository : Repository<Commande>, ICommandeRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public CommandeRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Commande>> GetAllActif()
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Commande>> GetAllInActif()
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Commande> GetByIdActif(int id)
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0 && a.IdCommande == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Commande>> GetByIdActifDoctor(int id)
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0 && a.IdDoctor == id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Commande>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Commande>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Commande>> GetAllPending()
        {
            var result = await MyDbContext.Commande.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Commande>> GetAllRejected()
        {
            var result = await MyDbContext.Commande.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Commande>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0 &&  a.IdDoctor==id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Commande>> GetByIdActifUser(int id)
        {
            var result = await MyDbContext.Commande.Where(a => a.Active == 0 && a.IdUser == id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Commande>> GetAllWithArtisteAsync()
        //{
        //    return await MyCommandeDbContext.Commandes
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
