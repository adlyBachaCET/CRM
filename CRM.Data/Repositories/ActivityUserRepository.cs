
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
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllInActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ActivityUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0 && a.IdUser == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllPending()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ActivityUser>> GetAllRejected()
        {
            var result = await MyDbContext.ActivityUser.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ActivityUser>> GetAllById(int Id)
        {
            var result = await MyDbContext.ActivityUser.Where(a =>a.Active==0 && a.IdActivity==Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<ActivityUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyActivityUserDbContext.ActivityUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
