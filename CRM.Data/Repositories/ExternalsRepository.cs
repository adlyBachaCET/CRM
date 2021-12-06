using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ExternalsRepository : Repository<Externals>, IExternalsRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ExternalsRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Externals>> GetAllActif()
        {
            var result = await MyDbContext.Externals.Where(a => a.Active == 0)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i=>i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Externals>> GetAllInActif()
        {
            var result = await MyDbContext.Externals.Where(a => a.Active == 1)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<Externals> GetByIdActif(int id)
        {
            var result = await MyDbContext.Externals.Where(a => a.Active == 0 && a.IdExternal == id)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Externals>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Externals.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Externals>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Externals.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Externals>> GetAllPending()
        {
            var result = await MyDbContext.Externals.Where(a => a.Status == Status.Pending)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Externals>> GetAllRejected()
        {
            var result = await MyDbContext.Externals.Where(a => a.Status == Status.Rejected)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Externals>> GetAllById(int Id)
        {
            var result = await MyDbContext.Externals.Where(a =>a.Active==0 && a.IdExternal==Id)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
  
 
      
    }
}
