using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class DelegateManagerRepository : Repository<DelegateManager>, IDelegateManagerRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public DelegateManagerRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<DelegateManager>> GetAllActif()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DelegateManager>> GetAllInActif()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<DelegateManager> GetByIdActif(int id)
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Active == 0 && a.IdDelegate == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<DelegateManager>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DelegateManager>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DelegateManager>> GetAllPending()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<DelegateManager>> GetAllRejected()
        {
            var result = await MyDbContext.DelegateManager.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<DelegateManager>> GetAllWithArtisteAsync()
        //{
        //    return await MyDelegateManagerDbContext.DelegateManagers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
