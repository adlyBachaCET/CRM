using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PharmacyRepository : Repository<Pharmacy>, IPharmacyRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PharmacyRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Pharmacy>> GetAllActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllInActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Pharmacy> GetByIdActif(int id)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.IdPharmacy == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllPending()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetAllRejected()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Pharmacy>> GetAllWithArtisteAsync()
        //{
        //    return await MyPharmacyDbContext.Pharmacys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
