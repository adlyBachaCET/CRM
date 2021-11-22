using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ProductRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetAllActif()
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllInActif()
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Product> GetByIdActif(int id)
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 0 && a.IdProduct == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllPending()
        {
            var result = await MyDbContext.Product.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Product>> GetAllRejected()
        {
            var result = await MyDbContext.Product.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<Product>> GetByIdPharmacy(int id)
        {
            var result = await MyDbContext.Product.Where(a => a.Active == 0 && a.IdProduct == id).ToListAsync();
            return result;
        }
     
        //public async Task<IEnumerable<Product>> GetAllWithArtisteAsync()
        //{
        //    return await MyProductDbContext.Products
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
