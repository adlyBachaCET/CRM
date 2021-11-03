using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class WholeSalerRepository : Repository<WholeSaler>, IWholeSalerRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public WholeSalerRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<WholeSaler>> GetAllActif()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSaler>> GetAllInActif()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<WholeSaler> GetByIdActif(int id)
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Active == 0 && a.IdPwholeSaler == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSaler>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSaler>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSaler>> GetAllPending()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<WholeSaler>> GetAllRejected()
        {
            var result = await MyDbContext.WholeSaler.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<WholeSaler>> GetAllWithArtisteAsync()
        //{
        //    return await MyWholeSalerDbContext.WholeSalers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
