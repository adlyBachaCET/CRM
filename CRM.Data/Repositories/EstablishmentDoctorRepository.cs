using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentDoctorRepository : Repository<EstablishmentDoctor>, IEstablishmentDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllActif()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<EstablishmentDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Active == 0 && a.IdDoctor == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EstablishmentDoctor>> GetAllPending()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EstablishmentDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.EstablishmentDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<EstablishmentDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentDoctorDbContext.EstablishmentDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
