using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SpecialtyRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Specialty>> GetAllActif()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Specialty>> GetAllInActif()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Specialty> GetByIdActif(int id)
        {
            var result = await MyDbContext.Specialty.Where(a => a.Active == 0 && a.IdSpecialty == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Specialty>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Specialty>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Specialty>> GetAllPending()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Specialty>> GetAllRejected()
        {
            var result = await MyDbContext.Specialty.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Specialty>> GetAllWithArtisteAsync()
        //{
        //    return await MySpecialtyDbContext.Specialtys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
