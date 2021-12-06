
using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ActivityUserRepository : Repository<ActivityUser>, IActivityUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ActivityUserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ActivityUser>> GetAllActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllInActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 1)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }
        public async Task<ActivityUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0 && a.IdUser == id)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllPending()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Status == Status.Pending)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ActivityUser>> GetAllRejected()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Status == Status.Rejected)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllById(int Id)
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0 && a.IdActivity == Id)
                .Include(a => a.User)
                .Include(a => a.Activity)
                .ToListAsync();
            return result;
        }

    }
}
