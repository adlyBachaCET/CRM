using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class LocationDoctorRepository : Repository<LocationDoctor>, ILocationDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public LocationDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<LocationDoctor>> GetAllActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<LocationDoctor> GetByIdLocationAndService(int id, int IdLocation)
        {
            return
                await MyDbContext.LocationDoctor.Where(i => i.IdService == id && i.IdLocation == IdLocation && i.Active == 0)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .FirstOrDefaultAsync();
        }
        public async Task<LocationDoctor> GetById(int idDoctor, int IdLocation)
        {
            return
                     await MyDbContext.LocationDoctor.Where(i => i.IdDoctor == idDoctor && i.IdLocation == IdLocation && i.Active == 0)
                     .Include(i => i.Service)
                     .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                     .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 1)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<LocationDoctor> GetByIdActif(int id,int IdLocation)
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.IdDoctor == id&& a.IdLocation== IdLocation)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif(int Id)
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.IdDoctor==Id)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllPending()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Status == Status.Pending)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i => i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Status == Status.Rejected)
                .Include(i => i.Service)
                .Include(i => i.Location).ThenInclude(i=>i.IdLocationTypeNavigation)
                .ToListAsync();
            return result;
        }
   
    }
}
