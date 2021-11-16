using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class VisitReportRepository : Repository<VisitReport>, IVisitReportRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public VisitReportRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<VisitReport>> GetAllActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllInActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<VisitReport> GetByIdActif(int id)
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.IdVisit == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllPending()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<VisitReport>> GetAllRejected()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllById(int Id)
        {
            var result = await MyDbContext.VisitReport.Where(a =>a.Active==0 && a.IdVisit==Id).ToListAsync();
            return result;
        }

        public async Task<VisitReport> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.Visit.IdDoctor == id).FirstOrDefaultAsync();
            return result;
        }
        //public async Task<IEnumerable<VisitReport>> GetAllWithArtisteAsync()
        //{
        //    return await MyVisitReportDbContext.VisitReports
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
