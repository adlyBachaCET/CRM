using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class CycleRepository : Repository<Cycle>, ICycleRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public CycleRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Cycle>> GetAllActif()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Cycle>> GetAllInActif()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Cycle> GetByIdActif(int id)
        {
            var result = await MyDbContext.Cycle.Where(a => a.Active == 0 && a.IdCycle == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Cycle>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Cycle>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Cycle>> GetAllPending()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Cycle>> GetAllRejected()
        {
            var result = await MyDbContext.Cycle.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Cycle>> GetAllWithArtisteAsync()
        //{
        //    return await MyCycleDbContext.Cycles
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
