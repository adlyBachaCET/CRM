using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public LocationTypeRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<LocationType>> GetAllActif()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationType>> GetAllInActif()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<LocationType> GetByIdActif(int id)
        {
            var result = await MyDbContext.LocationType.Where(a => a.Active == 0 && a.IdLocationType == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<LocationType>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationType>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationType>> GetAllPending()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationType>> GetAllRejected()
        {
            var result = await MyDbContext.LocationType.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<LocationType>> GetAllWithArtisteAsync()
        //{
        //    return await MyLocationTypeDbContext.LocationTypes
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
