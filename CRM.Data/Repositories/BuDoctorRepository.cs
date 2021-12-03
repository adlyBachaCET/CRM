using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class BuDoctorRepository : Repository<BuDoctor>, IBuDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public BuDoctorRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<BuDoctor> GetById(int idDoctor, int idBu)
        {
            return
                await MyDbContext.BuDoctor.Where(i => i.IdDoctor == idDoctor &&
                i.IdBu == idBu &&
                i.Active == 0).Include(p=>p.Bu).Include(p => p.IdDoctorNavigation)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BuDoctor>> GetAllActif()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Active == 0).Include(p => p.Bu).Include(p => p.IdDoctorNavigation).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Active == 1).Include(p => p.Bu).Include(p => p.IdDoctorNavigation).ToListAsync();
            return result;
        }
        public async Task<BuDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Active == 0 && a.IdBu == id)
                .Include(p => p.Bu).Include(p => p.IdDoctorNavigation)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<BuDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Active == 0 
            && a.Status == Status.Approuved)
           .Include(p => p.Bu).Include(p => p.IdDoctorNavigation).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(p => p.Bu).Include(p => p.IdDoctorNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BuDoctor>> GetAllPending()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Status == Status.Pending)
                .Include(p => p.Bu).Include(p => p.IdDoctorNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BuDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.Status == Status.Pending)
                .Include(p => p.Bu).Include(p => p.IdDoctorNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BuDoctor>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.BuDoctor.Where(a => a.IdDoctor == id && a.Status == Status.Pending)
                   .Include(p => p.Bu).Include(p => p.IdDoctorNavigation)
                   .ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<BuDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyBuDoctorDbContext.BuDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
