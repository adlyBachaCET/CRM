using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PotentielSectorRepository : Repository<PotentielSector>, IPotentielSectorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PotentielSectorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PotentielSector>> GetAllActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllInActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
    
        public async Task<PotentielSector> GetByIdActif(int id)
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0 && a.IdPotentiel == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PotentielSector>> GetAllPending()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PotentielSector>> GetAllRejected()
        {
            var result = await MyDbContext.PotentielSector.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<PotentielSector>> GetAllWithArtisteAsync()
        //{
        //    return await MyPotentielSectorDbContext.PotentielSectors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
