using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PhoneRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Phone>> GetAllActif()
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Phone>> GetAllInActif()
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Phone> GetByIdActif(int id)
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0 && a.IdPhone == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Phone>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Phone>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Phone>> GetAllPending()
        {
            var result = await MyDbContext.Phone.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Phone>> GetAllRejected()
        {
            var result = await MyDbContext.Phone.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Phone>> GetAllWithArtisteAsync()
        //{
        //    return await MyPhoneDbContext.Phones
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
