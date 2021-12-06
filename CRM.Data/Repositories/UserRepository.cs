using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public UserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<User>> GetAllActif()
        {
            var result = await MyDbContext.User.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetAllInActif()
        {
            var result = await MyDbContext.User.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<User> GetByIdActif(int id)
        {
            var result = await MyDbContext.User.Where(a => a.Active == 0 && a.IdUser == id).Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.ActivityUser)
                    .Include(i => i.Target)
                    .Include(i => i.Commande).ThenInclude(i=>i.Pharmacy)
                    .Include(i => i.Commande).ThenInclude(i => i.Doctor)
                    .Include(i => i.Objection)
                    .Include(i => i.DelegatesDotlineManager1)
                    .Include(i => i.DelegatesDotlineManager2)
                    .Include(i => i.DirectManager)
                    .Include(i => i.BuUser).ThenInclude(i=>i.Bu)
                    .Include(i => i.CycleUser).ThenInclude(i=>i.Cycle).ThenInclude(i=>i.PotentielCycle)
                    .Include(i => i.BuUser)
                    .Include(i => i.Locality1).ThenInclude(i => i.IdParentNavigation)
                    .Include(i => i.Locality2).ThenInclude(i => i.IdParentNavigation)
                    .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.User.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.User.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetAllPending()
        {
            var result = await MyDbContext.User.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<User>> GetAllRejected()
        {
            var result = await MyDbContext.User.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<User> GetByToken(string Token)
        {
            var result = await MyDbContext.User.Where(a => a.Active == 0 && a.GeneratedPassword == Token).FirstOrDefaultAsync();
            return result;
        }
    }
}
