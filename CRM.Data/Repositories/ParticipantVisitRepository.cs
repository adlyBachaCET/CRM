using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ParticipantVisitRepository : Repository<ParticipantVisit>, IParticipantVisitRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ParticipantVisitRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllActif()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllInActif()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ParticipantVisit> GetByIdActif(int id)
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0 && a.IdUser == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllPending()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllRejected()
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ParticipantVisit>> GetAllById(int Id)
        {
            var result = await MyDbContext.ParticipantVisit.Where(a =>a.Active==0 && a.IdUser==Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdDoctor(int Id)
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0 && a.IdDoctor == Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdPharmacy(int Id)
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0 && a.IdPharmacy == Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ParticipantVisit>> GetAllByIdVisitReport(int Id)
        {
            var result = await MyDbContext.ParticipantVisit.Where(a => a.Active == 0 && a.IdVisitReport == Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<ParticipantVisit>> GetAllWithArtisteAsync()
        //{
        //    return await MyParticipantVisitDbContext.ParticipantVisits
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
