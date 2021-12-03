using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class BuUserRepository : Repository<BuUser>, IBuUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public BuUserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BuUser>> GetAllActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0)
                .Include(i=>i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllInActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 1)
                .Include(i => i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<BuUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0 && a.IdBu == id)
                .Include(i => i.Bu).Include(i => i.User)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllPending()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Status == Status.Pending)
                .Include(i => i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BuUser>> GetAllRejected()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Status == Status.Rejected)
                .Include(i => i.Bu).Include(i => i.User)
                .ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<BuUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyBuUserDbContext.BuUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
