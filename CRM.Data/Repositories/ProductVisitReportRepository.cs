using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ProductVisitReportRepository : Repository<ProductVisitReport>, IProductVisitReportRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ProductVisitReportRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllActif()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 0)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllInActif()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 1)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
        public async Task<ProductVisitReport> GetByIdActif(int id)
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 0 && a.IdReport == id)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductVisitReport>> GetAllPending()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Status == Status.Pending)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ProductVisitReport>> GetAllRejected()
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Status == Status.Rejected)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<ProductVisitReport>> GetByIdVisitReport(int id)
        {
            var result = await MyDbContext.ProductVisitReport.Where(a => a.Active == 0 && a.IdReport == id)
                .Include(i => i.Report)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
     
  
    }
}
