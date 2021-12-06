using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PotentielSectorRepository : Repository<PotentielSector>, IPotentielSectorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PotentielSectorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PotentielSector>> GetAllActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0)
                 .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllInActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 1)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }
    
        public async Task<PotentielSector> GetByIdActif(int id)
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0 && a.IdPotentiel == id)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllPending()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Status == Status.Pending)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PotentielSector>> GetAllRejected()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Status == Status.Rejected)
                .Include(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation)
                .ToListAsync();
            return result;
        }
      
    }
}
