using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class CycleBuRepository : Repository<CycleBu>, ICycleBuRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public CycleBuRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CycleBu>> GetAllActif()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleBu>> GetAllInActif()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<CycleBu> GetByIdActif(int id)
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Active == 0 && a.IdBu == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<CycleBu>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleBu>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleBu>> GetAllPending()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<CycleBu>> GetAllRejected()
        {
            var result = await MyDbContext.CycleBu.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<CycleBu>> GetAllWithArtisteAsync()
        //{
        //    return await MyCycleBuDbContext.CycleBus
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
