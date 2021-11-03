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
            var result = await MyDbContext.User.Where(a => a.Active == 0 && a.IdUser == id).FirstOrDefaultAsync();
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
            var result = await MyDbContext.User.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
    }
}
