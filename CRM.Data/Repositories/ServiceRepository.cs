using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ServiceRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Service>> GetAllActif()
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Service>> GetAllInActif()
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 1).ToListAsync();
            return result;
        }



     
        public async Task<Service> GetByIdActif(int? id)
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 0 && a.IdService == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Service> GetByNameActif(string Name)
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 0 && a.Name == Name).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Service>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Service>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Service.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Service>> GetAllPending()
        {
            var result = await MyDbContext.Service.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Service>> GetAllRejected()
        {
            var result = await MyDbContext.Service.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Service>> GetAllWithArtisteAsync()
        //{
        //    return await MyServiceDbContext.Services
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
