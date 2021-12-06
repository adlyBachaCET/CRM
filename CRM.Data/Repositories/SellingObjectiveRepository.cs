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
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0)
           .Include(i => i.Product)
           .Include(i => i.Pharmacy)
           .Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllInActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 1)
                .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<SellingObjectives> GetByIdActif(int id)
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.IdSellingObjectives == id)
                     .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                .Include(i => i.Doctor)
                .Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllPending()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Status == Status.Pending)
                     .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SellingObjectives>> GetAllRejected()
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Status == Status.Rejected)
                     .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .ToListAsync();
            return result;
        }

     
        public async Task<IEnumerable<SellingObjectives>> GetByIdSellingObjectives(int id)
        {
            var result = await MyDbContext.SellingObjectives.Where(a => a.Active == 0 && a.IdSellingObjectives == id)
                     .Include(i => i.Product)
                .Include(i => i.Pharmacy)
                 .Include(i => i.Doctor)
                 .Include(i => i.User)
                .ToListAsync();
            return result;
        }
     

    }
}
