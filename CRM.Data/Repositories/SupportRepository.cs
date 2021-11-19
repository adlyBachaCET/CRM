using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SupportRepository : Repository<Support>, ISupportRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SupportRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Support>> GetAllActif()
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Support>> GetAllInActif()
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 1).ToListAsync();
            return result;
        }



     
        public async Task<Support> GetByIdActif(int? id)
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 0 && a.IdSupport == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Support> GetByNameActif(string Name)
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 0 && a.Name == Name).SingleOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Support>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Support>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Support.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Support>> GetAllPending()
        {
            var result = await MyDbContext.Support.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Support>> GetAllRejected()
        {
            var result = await MyDbContext.Support.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Support>> GetAllWithArtisteAsync()
        //{
        //    return await MySupportDbContext.Supports
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
