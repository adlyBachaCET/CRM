using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public VisitRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Visit>> GetAllActif()
        {
            var result = await MyDbContext.Visit.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Visit>> GetAllInActif()
        {
            var result = await MyDbContext.Visit.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Visit> GetByIdActif(int id)
        {
            var result = await MyDbContext.Visit.Where(a => a.Active == 0 && a.IdVisit == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Visit>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Visit.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Visit>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Visit.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Visit>> GetAllPending()
        {
            var result = await MyDbContext.Visit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Visit>> GetAllRejected()
        {
            var result = await MyDbContext.Visit.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Visit>> GetAllById(int Id)
        {
            var result = await MyDbContext.Visit.Where(a =>a.Active==0 && a.IdDoctor==Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Visit>> GetAllWithArtisteAsync()
        //{
        //    return await MyVisitDbContext.Visits
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
