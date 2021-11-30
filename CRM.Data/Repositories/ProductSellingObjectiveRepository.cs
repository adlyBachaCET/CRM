using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ProductSellingObjectivesRepository : Repository<ProductSellingObjectives>, IProductSellingObjectivesRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ProductSellingObjectivesRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllActif()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllInActif()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<ProductSellingObjectives> GetByIdActif(int id)
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 0 && a.IdProduct == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllPending()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ProductSellingObjectives>> GetAllRejected()
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<ProductSellingObjectives>> GetByIdSellingObjectives(int id)
        {
            var result = await MyDbContext.ProductSellingObjectives.Where(a => a.Active == 0 && a.IdSellingObjectives == id).ToListAsync();
            return result;
        }
     
        //public async Task<IEnumerable<ProductSellingObjectives>> GetAllWithArtisteAsync()
        //{
        //    return await MyProductSellingObjectivesDbContext.ProductSellingObjectivess
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
