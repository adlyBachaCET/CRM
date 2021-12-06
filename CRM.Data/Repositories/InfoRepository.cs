using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class InfoRepository : Repository<Info>, IInfoRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public InfoRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Info>> GetAllActif()
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Info>> GetAllInActif()
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Info> GetByIdActif(int id)
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 0 && a.IdInf == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Info>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Info>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Info>> GetAllPending()
        {
            var result = await MyDbContext.Info.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Info>> GetAllRejected()
        {
            var result = await MyDbContext.Info.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Info>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.Info.Where(a => a.Active == 0 &&  a.IdDoctor==id).ToListAsync();
            return result;
        }
 
    }
}
