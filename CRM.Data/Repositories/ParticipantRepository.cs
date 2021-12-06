using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ParticipantRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Participant>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.Participant.Where(a => a.IdDoctor==id && a.Active == 0)
              .Include(i => i.IdUserNavigation)
              .Include(i => i.IdRequestRpNavigation)
              .Include(i => i.IdPharmacyNavigation)
              .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
              .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0)
                .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i=>i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllInActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 1)
                .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<Participant> GetByIdActif(int id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdUser == id)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                 .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllPending()
        {
            var result = await MyDbContext.Participant.Where(a => a.Status == Status.Pending)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllRejected()
        {
            var result = await MyDbContext.Participant.Where(a => a.Status == Status.Rejected)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllById(int Id)
        {
            var result = await MyDbContext.Participant.Where(a =>a.Active==0 && a.IdUser==Id)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllByIdDoctor(int Id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdDoctor == Id)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllByIdPharmacy(int Id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdPharmacy == Id)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllByIdRequest(int Id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdRequestRp == Id)
                  .Include(i => i.IdUserNavigation)
                .Include(i => i.IdRequestRpNavigation)
                .Include(i => i.IdPharmacyNavigation)
                .Include(i => i.IdVisitReportNavigation).ThenInclude(i => i.Visit)
                .ToListAsync();
            return result;
        }
      
    }
}
