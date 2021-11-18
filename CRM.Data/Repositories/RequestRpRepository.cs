using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class RequestRpRepository : Repository<RequestRp>, IRequestRpRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public RequestRpRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<RequestRp>> GetAllActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllInActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<RequestRp> GetByIdActif(int id)
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.IdRequestRp== id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllPending()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RequestRp>> GetAllRejected()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async  Task<RequestRp> GetByNames(string Names)
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.Name== Names).FirstOrDefaultAsync();
            return result;
        }

        //public async Task<IEnumerable<RequestRp>> GetAllWithArtisteAsync()
        //{
        //    return await MyRequestRpDbContext.RequestRps
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
