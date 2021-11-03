using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PharmacyLocalityRepository : Repository<PharmacyLocality>, IPharmacyLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PharmacyLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllActif()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllInActif()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<PharmacyLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PharmacyLocality>> GetAllPending()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PharmacyLocality>> GetAllRejected()
        {
            var result = await MyDbContext.PharmacyLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<PharmacyLocality>> GetAllWithArtisteAsync()
        //{
        //    return await MyPharmacyLocalityDbContext.PharmacyLocalitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
