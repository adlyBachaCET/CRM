using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class WholeSalerLocalityRepository : Repository<WholeSalerLocality>, IWholeSalerLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public WholeSalerLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllActif()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllInActif()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<WholeSalerLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WholeSalerLocality>> GetAllPending()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<WholeSalerLocality>> GetAllRejected()
        {
            var result = await MyDbContext.WholeSalerLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<WholeSalerLocality>> GetAllWithArtisteAsync()
        //{
        //    return await MyWholeSalerLocalityDbContext.WholeSalerLocalitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
