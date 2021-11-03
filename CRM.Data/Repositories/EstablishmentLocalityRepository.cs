using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentLocalityRepository : Repository<EstablishmentLocality>, IEstablishmentLocalityRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentLocalityRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllActif()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllInActif()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<EstablishmentLocality> GetByIdActif(int id)
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Active == 0 && a.IdLocality == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentLocality>> GetAllPending()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EstablishmentLocality>> GetAllRejected()
        {
            var result = await MyDbContext.EstablishmentLocality.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<EstablishmentLocality>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentLocalityDbContext.EstablishmentLocalitys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
