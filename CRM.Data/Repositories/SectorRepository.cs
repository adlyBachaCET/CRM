using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SectorRepository : Repository<Sector>, ISectorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SectorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Sector>> GetAllActif()
        {
            var result = await MyDbContext.Sector.Where(a => a.Active == 0)
                .Include(i => i.SectorLocality).ThenInclude(i=>i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Sector>> GetAllInActif()
        {
            var result = await MyDbContext.Sector.Where(a => a.Active == 1)
                .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<Sector> GetByIdActif(int id)
        {
            var result = await MyDbContext.Sector.Where(a => a.Active == 0 && a.IdSector == id)
                .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Sector>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Sector.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Sector>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Sector.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Sector>> GetAllPending()
        {
            var result = await MyDbContext.Sector.Where(a => a.Status == Status.Pending)
                .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Sector>> GetAllRejected()
        {
            var result = await MyDbContext.Sector.Where(a => a.Status == Status.Rejected)
           .Include(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
           .ToListAsync();
            return result;
        }
 
    }
}
