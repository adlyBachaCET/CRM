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
            var result = await MyDbContext.Location.Where(a => a.Active == 0)
                .Include(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Location>> GetAllByType(int TypeName)
        {
            return
                           await MyDbContext.Location.Where(i => i.IdLocationType == TypeName && i.Active == 0).Include(i => i.IdLocationTypeNavigation)
                .ToListAsync();
        }
        public async Task<IEnumerable<Service>> GetAllServices(int Id)
        {
            var ServicesLocation = await MyDbContext.LocationDoctor.Where(i => i.IdLocation == Id && i.IdDoctor == null && i.Active == 0)
                      .Include(i => i.Service)
                     .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                     .ToListAsync();
            IEnumerable<Service> Services = ServicesLocation.Select(i => i.Service);
            return Services;
        }
        public async Task<IEnumerable<Location>> GetAllInActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 1).Include(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<Location> GetByIdActif(int id)
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.IdLocation == id).Include(i => i.IdLocationTypeNavigation)
            .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.Status == Status.Approuved).Include(i => i.IdLocationTypeNavigation)
            .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllPending()
        {
            var result = await MyDbContext.Location.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Location>> GetAllRejected()
        {
            var result = await MyDbContext.Location.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }
        public async Task<Location> GetByExistantActif(string Name, int? IdlocationType)
        {
            var LocationType = await MyDbContext.LocationType.Where(a => a.Active == 0 && a.IdLocationType == IdlocationType).FirstOrDefaultAsync();

            var Location = await MyDbContext.Location.Where(a => a.Active == 0 && a.Name == Name && a.IdLocationType == LocationType.IdLocationType)
             .Include(i => i.IdLocationTypeNavigation)
            .FirstOrDefaultAsync();
            return Location;
        }
        public async Task<Location> GetByExistantNameActif(string Name)
        {
            var result = await MyDbContext.Location.Where(a => a.Active == 0 && a.Name == Name)
                .Include(i => i.IdLocationTypeNavigation)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Location> GetByExactExistantActif(string Name, int PostalCode, string Locality1, string Locality2)
        {
            var Location = await MyDbContext.Location.Where(a => a.Active == 0
            && a.NameLocality1 == Locality1
            && a.PostalCode == PostalCode
            && a.Name == Name
            && a.NameLocality2 == Locality2 &&
            a.PostalCode == PostalCode)
            .Include(i => i.IdLocationTypeNavigation)
            .FirstOrDefaultAsync();
            return Location;
        }
        public async Task<IEnumerable<Location>> GetByNearByActif(string Locality1, string Locality2, int CodePostal)
        {
            var Location = await MyDbContext.Location.Where(a => a.Active == 0 && a.NameLocality1 == Locality1

            && a.NameLocality2 == Locality2 &&
            a.PostalCode == CodePostal).Include(i => i.IdLocationTypeNavigation)
            .ToListAsync();
            return Location;
        }
   
    }
}
