using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentUserRepository : Repository<EstablishmentUser>, IEstablishmentUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentUserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllActif()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllInActif()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<EstablishmentUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Active == 0 && a.IdEstablishment == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentUser>> GetAllPending()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EstablishmentUser>> GetAllRejected()
        {
            var result = await MyDbContext.EstablishmentUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<EstablishmentUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentUserDbContext.EstablishmentUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
