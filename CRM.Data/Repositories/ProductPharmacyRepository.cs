using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ProductPharmacyRepository : Repository<ProductPharmacy>, IProductPharmacyRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ProductPharmacyRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllActif()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllInActif()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ProductPharmacy> GetByIdActif(int id)
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 0 && a.IdPharmacy == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllPending()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ProductPharmacy>> GetAllRejected()
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<ProductPharmacy>> GetByIdPharmacy(int id)
        {
            var result = await MyDbContext.ProductPharmacy.Where(a => a.Active == 0 && a.IdPharmacy == id).ToListAsync();
            return result;
        }
     
        //public async Task<IEnumerable<ProductPharmacy>> GetAllWithArtisteAsync()
        //{
        //    return await MyProductPharmacyDbContext.ProductPharmacys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
