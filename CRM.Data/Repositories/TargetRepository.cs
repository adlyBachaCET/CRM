using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class TargetRepository : Repository<Target>, ITargetRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public TargetRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Target>> GetAllActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllInActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Target> GetByIdActif(int id)
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.IdCycle == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllPending()
        {
            var result = await MyDbContext.Target.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Target>> GetAllRejected()
        {
            var result = await MyDbContext.Target.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Target>> GetAllWithArtisteAsync()
        //{
        //    return await MyTargetDbContext.Targets
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
