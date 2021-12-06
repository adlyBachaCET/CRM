using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SectorCycleRepository : Repository<SectorCycle>, ISectorCycleRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SectorCycleRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SectorCycle>> GetAllActif()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycle>> GetAllInActif()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Active == 1)
                  .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<SectorCycle> GetByIdActif(int id)
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Active == 0 && a.IdCycle == id)
                 .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycle>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                 .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycle>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                 .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycle>> GetAllPending()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Status == Status.Pending)
                 .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SectorCycle>> GetAllRejected()
        {
            var result = await MyDbContext.SectorCycle.Where(a => a.Status == Status.Rejected)
                 .Include(i => i.IdSectorNavigation)
                 .Include(i => i.IdCycleNavigation)
                .ToListAsync();
            return result;
        } 
    }
}
