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
        public async Task<Location> GetByExistantActif(string Name,int? IdlocationType)
        {
            var LocationType = await MyDbContext.LocationType.Where(a => a.Active == 0 && a.IdLocationType == IdlocationType).FirstOrDefaultAsync();

            var Location = await MyDbContext.Location.Where(a => a.Active == 0 && a.Name == Name&& a.IdLocationType== LocationType.IdLocationType).FirstOrDefaultAsync();
            return Location;
        }
        public async Task<Location> GetByExistantNameActif(string Name)
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.Name == Name).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Location>> GetByExactExistantActif(string Name,int PostalCode ,string Locality1, string Locality2)
        {
            var Location = await MyDbContext.Location.Where(a => a.Active == 0 
            && a.NameLocality1 == Locality1
            && a.PostalCode == PostalCode 
            && a.Name==Name
            && a.NameLocality2 == Locality2 &&
            a.PostalCode == PostalCode).ToListAsync();
            return Location;
        }
        public async Task<IEnumerable<Location>> GetByNearByActif(string Locality1, string Locality2, int CodePostal)
        {
            var Location = await MyDbContext.Location.Where(a => a.Active == 0 && a.NameLocality1 == Locality1

            && a.NameLocality2 == Locality2  &&
            a.PostalCode == CodePostal).ToListAsync();
            return Location;
        }
        //public async Task<IEnumerable<Location>> GetAllWithArtisteAsync()
        //{
        //    return await MyLocationDbContext.Locations
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
