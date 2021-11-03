using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class WeekSectorCycleInYearRepository : Repository<WeekSectorCycleInYear>, IWeekSectorCycleInYearRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public WeekSectorCycleInYearRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllActif()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllInActif()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<WeekSectorCycleInYear> GetByIdActif(int id)
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Active == 0 && a.Order == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllPending()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<WeekSectorCycleInYear>> GetAllRejected()
        {
            var result = await MyDbContext.WeekSectorCycleInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        } 
    }
}
