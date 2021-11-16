using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ObjectionRepository : Repository<Objection>, IObjectionRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ObjectionRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Objection>> GetAllActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllInActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Objection> GetByIdActif(int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.IdObjection == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetByIdActifDoctor(int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.IdDoctor == id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllPending()
        {
            var result = await MyDbContext.Objection.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetAllRejected()
        {
            var result = await MyDbContext.Objection.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 &&  a.IdDoctor==id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Objection>> GetAllWithArtisteAsync()
        //{
        //    return await MyObjectionDbContext.Objections
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
