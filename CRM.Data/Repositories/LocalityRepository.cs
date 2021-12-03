using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class LocalityRepository : Repository<Locality>, ILocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public LocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Locality>> GetAllActif()
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Locality>> GetAllActifLVL1()
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0&& a.Lvl==1).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Locality>> GetAllActifLVL2(int id)
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0&& a.Lvl==2&& a.IdParent==id).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Locality>> GetAllInActif()
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Locality> GetByIdActif(int id)
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Locality> GetByIdActif(int? id)
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Locality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Locality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Locality.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Locality>> GetAllPending()
        {
            var result = await MyDbContext.Locality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Locality>> GetAllRejected()
        {
            var result = await MyDbContext.Locality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Locality>> GetAllWithArtisteAsync()
        //{
        //    return await MyLocalityDbContext.Localitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
