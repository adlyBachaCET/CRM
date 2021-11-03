using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PotentielCycleRepository : Repository<PotentielCycle>, IPotentielCycleRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PotentielCycleRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PotentielCycle>> GetAllActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllInActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
    
        public async Task<PotentielCycle> GetByIdActif(int id)
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0 && a.IdPotentiel == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllPending()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PotentielCycle>> GetAllRejected()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<PotentielCycle>> GetAllWithArtisteAsync()
        //{
        //    return await MyPotentielCycleDbContext.PotentielCycles
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
