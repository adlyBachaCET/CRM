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
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0)
                   .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllInActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 1)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .ToListAsync();
            return result;
        }
    
        public async Task<PotentielCycle> GetByIdActif(int id)
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0 && a.IdPotentiel == id)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielCycle>> GetAllPending()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Status == Status.Pending)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PotentielCycle>> GetAllRejected()
        {
            var result = await MyDbContext.PotentielCycle.Where(a => a.Status == Status.Rejected)
                    .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdPotentielNavigation)
                .ToListAsync();
            return result;
        }

    }
}
