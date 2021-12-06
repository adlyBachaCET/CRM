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
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0)
                .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i=>i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i=>i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllInActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 1)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<RequestRp> GetByIdActif(int id)
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.IdRequestRp== id)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestRp>> GetAllPending()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Status == Status.Pending)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RequestRp>> GetAllRejected()
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Status == Status.Rejected)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .ToListAsync();
            return result;
        }

        public async  Task<RequestRp> GetByNames(string Names)
        {
            var result = await MyDbContext.RequestRp.Where(a => a.Active == 0 && a.Name== Names)
                  .Include(i => i.Participant).ThenInclude(i => i.IdUserNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                .Include(i => i.Participant).ThenInclude(i => i.IdRequestRpNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .Include(i => i.TagsRequestRp).ThenInclude(i => i.IdTagsNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

   
    }
}
