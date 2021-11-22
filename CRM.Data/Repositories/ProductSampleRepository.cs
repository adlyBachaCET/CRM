using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ProductSampleRepository : Repository<ProductSample>, IProductSampleRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ProductSampleRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProductSample>> GetAllActif()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSample>> GetAllInActif()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ProductSample> GetByIdActif(int id)
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 0 && a.IdProductSample == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSample>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSample>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSample>> GetAllPending()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ProductSample>> GetAllRejected()
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<ProductSample>> GetByIdPharmacy(int id)
        {
            var result = await MyDbContext.ProductSample.Where(a => a.Active == 0 && a.IdProductSample == id).ToListAsync();
            return result;
        }
     
        //public async Task<IEnumerable<ProductSample>> GetAllWithArtisteAsync()
        //{
        //    return await MyProductSampleDbContext.ProductSamples
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
