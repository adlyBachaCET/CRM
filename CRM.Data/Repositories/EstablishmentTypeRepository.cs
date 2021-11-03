using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentTypeRepository : Repository<EstablishmentType>, IEstablishmentTypeRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentTypeRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EstablishmentType>> GetAllActif()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllInActif()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<EstablishmentType> GetByIdActif(int id)
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Active == 0 && a.IdEstablishmentType == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentType>> GetAllPending()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EstablishmentType>> GetAllRejected()
        {
            var result = await MyDbContext.EstablishmentType.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<EstablishmentType>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentTypeDbContext.EstablishmentTypes
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
