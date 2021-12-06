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
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0)
              .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllInActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 1)
                  .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<VisitReport> GetByIdActif(int id)
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.IdVisit == id)
                    .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)

                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                   .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllPending()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Status == Status.Pending)
                   .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<VisitReport>> GetAllRejected()
        {
            var result = await MyDbContext.VisitReport.Where(a => a.Status == Status.Rejected)
                  .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetAllById(int Id)
        {
            var result = await MyDbContext.VisitReport.Where(a =>a.Active==0 && a.IdVisit==Id)
                    .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReport>> GetByIdDoctor(int id)
        {
            List<VisitReport> VisitReport = new List<VisitReport>();
            var Visit = await MyDbContext.Visit.Where(a => a.Active == 0 && a.IdDoctor == id).ToListAsync();
            foreach(var item in Visit) {
            var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.IdVisit == item.IdVisit)
                             .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                    .FirstOrDefaultAsync();
                VisitReport.Add(result);
            }

            return VisitReport;
        }

        public async Task<IEnumerable<VisitReport>> GetByIdPharmacy(int id)
        {
            List<VisitReport> VisitReport = new List<VisitReport>();
            var Visit = await MyDbContext.Visit.Where(a => a.Active == 0 && a.IdPharmacy == id).ToListAsync();
            foreach (var item in Visit)
            {
                var result = await MyDbContext.VisitReport.Where(a => a.Active == 0 && a.IdVisit == item.IdVisit)
                        .Include(i => i.Visit).ThenInclude(i => i.VisitUser).ThenInclude(i => i.User)
                 .Include(i => i.Visit).ThenInclude(i => i.Pharmacy)
                .Include(i => i.Visit).ThenInclude(i => i.Doctor)
                .Include(i => i.Visit).ThenInclude(i => i.Locality1)
                 .Include(i => i.Visit).ThenInclude(i => i.Locality2)
                 .Include(i => i.Participant).ThenInclude(i => i.IdPharmacyNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdDoctorNavigation)
                 .Include(i => i.Participant).ThenInclude(i => i.IdVisitReportNavigation)
                    .FirstOrDefaultAsync();
                VisitReport.Add(result);
            }

            return VisitReport;
        }
 
    }
}
