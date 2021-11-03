using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class WeekInYearRepository : Repository<WeekInYear>, IWeekInYearRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public WeekInYearRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<WeekInYear>> GetAllActif()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInYear>> GetAllInActif()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<WeekInYear> GetByIdActif(int id)
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Active == 0 && a.Order == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInYear>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInYear>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WeekInYear>> GetAllPending()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<WeekInYear>> GetAllRejected()
        {
            var result = await MyDbContext.WeekInYear.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<WeekInYear>> GetAllWithArtisteAsync()
        //{
        //    return await MyWeekInYearDbContext.WeekInYears
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
