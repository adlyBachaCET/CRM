
using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ActivityRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<List<Activity>> GetByIdUser(int id)
        {
            List<Activity> Activities = new List<Activity>();
            var List = await MyDbContext.ActivityUser.Where(i => i.IdUser == id && i.Active == 0).ToListAsync();
            foreach (var item in List)
            {
                var Activity = await MyDbContext.Activity.Where(i => i.IdActivity == item.IdActivity && i.Active == 0)
                    .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                    .FirstOrDefaultAsync();

                Activities.Add(Activity);
            }
            return
                  Activities;
        }
        public async Task<List<Activity>> GetByIdUserByToday(int id)
        {
            List<Activity> Activities = new List<Activity>();
            var List = await MyDbContext.ActivityUser.Where(i => i.IdUser == id && i.Active == 0).ToListAsync();
            foreach (var item in List)
            {
                var Activity = await MyDbContext.Activity.Where(i => i.IdActivity == item.IdActivity && i.Active == 0 && i.Start.Date == DateTime.Now.Date)
                    .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                    .FirstOrDefaultAsync();

                Activities.Add(Activity);
            }
            return
                  Activities;
        }
        public async Task<IEnumerable<Activity>> GetAllActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0)
                .Include(a => a.ActivityUser).ThenInclude(i=>i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllInActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 1)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<Activity> GetByIdActif(int id)
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0 && a.IdActivity == id)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllPending()
        {
            var result = await MyDbContext.Activity.Where(a => a.Status == Status.Pending)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Activity>> GetAllRejected()
        {
            var result = await MyDbContext.Activity.Where(a => a.Status == Status.Rejected)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllById(int Id)
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0 && a.IdActivity == Id)
                .Include(a => a.ActivityUser).ThenInclude(i => i.User)
                .ToListAsync();
            return result;
        }

    }
}
