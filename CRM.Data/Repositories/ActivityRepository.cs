
using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Activity>> GetAllActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllInActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Activity> GetByIdActif(int id)
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0 && a.IdActivity == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Activity.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllPending()
        {
            var result = await MyDbContext.Activity.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Activity>> GetAllRejected()
        {
            var result = await MyDbContext.Activity.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Activity>> GetAllById(int Id)
        {
            var result = await MyDbContext.Activity.Where(a =>a.Active==0 && a.IdActivity==Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Activity>> GetAllWithArtisteAsync()
        //{
        //    return await MyActivityDbContext.Activitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
