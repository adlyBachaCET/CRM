using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class AdresseRepository : Repository<Adresse>, IAdresseRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public AdresseRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Adresse>> GetAllActif()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Adresse>> GetAllInActif()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Adresse> GetByIdActif(int id)
        {
            var result = await MyDbContext.Adresses.Where(a => a.Active == 0 && a.IdAdresse == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Adresse>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Adresse>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Adresse>> GetAllPending()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Adresse>> GetAllRejected()
        {
            var result = await MyDbContext.Adresses.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Adresse>> GetAllWithArtisteAsync()
        //{
        //    return await MyAdresseDbContext.Adresses
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
