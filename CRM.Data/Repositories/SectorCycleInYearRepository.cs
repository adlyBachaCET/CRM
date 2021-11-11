using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SectorCycleInYearRepository : Repository<SectorCycleInYear>, ISectorCycleInYearRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SectorCycleInYearRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllInActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<SectorCycleInYear> GetByIdActif(int id)
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0 && a.Order == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SectorCycleInYear>> GetAllPending()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SectorCycleInYear>> GetAllRejected()
        {
            var result = await MyDbContext.SectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        } 
    }
}
