using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class BrickLocalityRepository : Repository<BrickLocality>, IBrickLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public BrickLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BrickLocality>> GetAllActif()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BrickLocality>> GetAllInActif()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<BrickLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Active == 0 && a.IdBrick == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<BrickLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BrickLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BrickLocality>> GetAllPending()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BrickLocality>> GetAllRejected()
        {
            var result = await MyDbContext.BrickLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<BrickLocalityLocality>> GetAllWithArtisteAsync()
        //{
        //    return await MyBrickLocalityDbContext.BrickLocalitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
