using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PotentielRepository : Repository<Potentiel>, IPotentielRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PotentielRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Potentiel>> GetAllActif()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Potentiel>> GetAllInActif()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

   
        public async Task<Potentiel> GetByIdActif(int id)
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Active == 0 && a.IdPotentiel == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Potentiel>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Potentiel>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Potentiel>> GetAllPending()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Potentiel>> GetAllRejected()
        {
            var result = await MyDbContext.Potentiel.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Potentiel>> GetAllWithArtisteAsync()
        //{
        //    return await MyPotentielDbContext.Potentiels
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
