using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class BusinessUnitRepository : Repository<BusinessUnit>, IBusinessUnitRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public BusinessUnitRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BusinessUnit>> GetAllActif()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllInActif()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<BusinessUnit> GetByIdActif(int id)
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Active == 0 && a.IdBu == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllPending()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BusinessUnit>> GetAllRejected()
        {
            var result = await MyDbContext.BusinessUnit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<BusinessUnit>> GetAllWithArtisteAsync()
        //{
        //    return await MyBusinessUnitDbContext.BusinessUnits
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
