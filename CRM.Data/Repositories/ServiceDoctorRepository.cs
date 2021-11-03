using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ServiceDoctorRepository : Repository<ServiceDoctor>, IServiceDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ServiceDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllActif()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ServiceDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Active == 0 && a.IdService == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ServiceDoctor>> GetAllPending()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ServiceDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.ServiceDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<ServiceDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyServiceDoctorDbContext.ServiceDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
