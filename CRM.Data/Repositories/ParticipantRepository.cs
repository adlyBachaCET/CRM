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

        public async Task<IEnumerable<Participant>> GetAllActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllInActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Participant> GetByIdActif(int id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdUser == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllPending()
        {
            var result = await MyDbContext.Participant.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllRejected()
        {
            var result = await MyDbContext.Participant.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllById(int Id)
        {
            var result = await MyDbContext.Participant.Where(a =>a.Active==0 && a.IdUser==Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllByIdDoctor(int Id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdDoctor == Id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Participant>> GetAllByIdRequest(int Id)
        {
            var result = await MyDbContext.Participant.Where(a => a.Active == 0 && a.IdRequestRp == Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Participant>> GetAllWithArtisteAsync()
        //{
        //    return await MyParticipantDbContext.Participants
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
