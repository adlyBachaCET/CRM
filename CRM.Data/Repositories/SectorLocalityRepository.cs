using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SectorLocalityRepository : Repository<SectorLocality>, ISectorLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SectorLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SectorLocality>> GetAllActif()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Active == 0)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorLocality>> GetAllInActif()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Active == 1)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<SectorLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Active == 0 && a.IdSector == id)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SectorLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorLocality>> GetAllPending()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Status == Status.Pending)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SectorLocality>> GetAllRejected()
        {
            var result = await MyDbContext.SectorLocality.Where(a => a.Status == Status.Rejected)
                .Include(i => i.IdSectorNavigation)
                .Include(i => i.IdLocalityNavigation)
                .ToListAsync();
            return result;
        }
    }
}
