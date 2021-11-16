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
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<LocationDoctor> GetByIdActif(int IdDoctor,int IdLocation)
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.IdDoctor == IdDoctor&& a.IdLocation== IdLocation).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif(int Id)
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.IdDoctor==Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LocationDoctor>> GetAllPending()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LocationDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.LocationDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<LocationDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyLocationDoctorDbContext.LocationDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
