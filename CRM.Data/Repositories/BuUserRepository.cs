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
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllInActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<BuUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0 && a.IdBu == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuUser>> GetAllPending()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BuUser>> GetAllRejected()
        {
            var result = await MyDbContext.BuUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<BuUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyBuUserDbContext.BuUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
