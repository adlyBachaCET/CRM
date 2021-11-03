using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class CycleSectorWeekDoctorsRepository : Repository<CycleSectorWeekDoctors>, ICycleSectorWeekDoctorsRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public CycleSectorWeekDoctorsRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllActif()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllInActif()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<CycleSectorWeekDoctors> GetByIdActif(int id)
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Active == 0 && a.IdCycle == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllPending()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllRejected()
        {
            var result = await MyDbContext.CycleSectorWeekDoctors.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<CycleSectorWeekDoctors>> GetAllWithArtisteAsync()
        //{
        //    return await MyCycleSectorWeekDoctorsDbContext.CycleSectorWeekDoctorss
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
