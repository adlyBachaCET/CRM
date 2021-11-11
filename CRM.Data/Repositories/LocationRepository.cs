using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public LocationRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Location>> GetAllActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllInActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
      
        public async Task<Location> GetByIdActif(int id)
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.IdLocation == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllPending()
        {
            var result = await MyDbContext.Location.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Location>> GetAllRejected()
        {
            var result = await MyDbContext.Location.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Location>> GetAllWithArtisteAsync()
        //{
        //    return await MyLocationDbContext.Locations
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
