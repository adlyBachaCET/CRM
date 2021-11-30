using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SellingObjectivesRepository : Repository<SellingObjectives>, ISellingObjectivesRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SellingObjectivesRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SellingObjectives>> GetAllActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllInActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<SellingObjectives> GetByIdActif(int id)
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.IdSellingObjectives == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllPending()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SellingObjectives>> GetAllRejected()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<SellingObjectives>> GetByIdSellingObjectives(int id)
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.IdSellingObjectives == id).ToListAsync();
            return result;
        }
     
        //public async Task<IEnumerable<SellingObjectives>> GetAllWithArtisteAsync()
        //{
        //    return await MySellingObjectivesDbContext.SellingObjectivess
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
