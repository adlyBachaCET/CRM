using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class VisitUserRepository : Repository<VisitUser>, IVisitUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public VisitUserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<VisitUser>> GetAllActif()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitUser>> GetAllInActif()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<VisitUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Active == 0 && a.IdVisit == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<VisitUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitUser>> GetAllPending()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<VisitUser>> GetAllRejected()
        {
            var result = await MyDbContext.VisitUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VisitUser>> GetAllById(int Id)
        {
            var result = await MyDbContext.VisitUser.Where(a =>a.Active==0 && a.IdUser==Id).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<VisitUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyVisitUserDbContext.VisitUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
