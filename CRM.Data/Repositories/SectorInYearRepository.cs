using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SectorInYearRepository : Repository<SectorInYear>, ISectorInYearRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SectorInYearRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SectorInYear>> GetAllActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorInYear>> GetAllInActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<SectorInYear> GetByIdActif(int id)
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0 && a.Order == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SectorInYear>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorInYear>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorInYear>> GetAllPending()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SectorInYear>> GetAllRejected()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        } 
    }
}
