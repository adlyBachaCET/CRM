using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class WeekInCycleRepository : Repository<WeekInCycle>, IWeekInCycleRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public WeekInCycleRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<WeekInCycle>> GetAllActif()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllInActif()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<WeekInCycle> GetByIdActif(int id)
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Active == 0 && a.IdWeekCycle == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInCycle>> GetAllPending()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<WeekInCycle>> GetAllRejected()
        {
            var result = await MyDbContext.WeekInCycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
    }
}
