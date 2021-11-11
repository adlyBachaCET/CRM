using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class AdresseLocalityRepository : Repository<AdresseLocality>, IAdresseLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public AdresseLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<AdresseLocality>> GetAllActif()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllInActif()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<AdresseLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AdresseLocality>> GetAllPending()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<AdresseLocality>> GetAllRejected()
        {
            var result = await MyDbContext.AdresseLocalitys.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<AdresseLocality>> GetAllWithArtisteAsync()
        //{
        //    return await MyAdresseLocalityDbContext.AdresseLocalitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
