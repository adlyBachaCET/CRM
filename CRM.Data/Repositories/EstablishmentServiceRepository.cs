using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentServiceRepository : Repository<EstablishmentService>, IEstablishmentServiceRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentServiceRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EstablishmentService>> GetAllActif()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllInActif()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<EstablishmentService> GetByIdActif(int id)
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Active == 0 && a.IdEstablishment == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentService>> GetAllPending()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EstablishmentService>> GetAllRejected()
        {
            var result = await MyDbContext.EstablishmentService.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<EstablishmentService>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentServiceDbContext.EstablishmentServices
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
