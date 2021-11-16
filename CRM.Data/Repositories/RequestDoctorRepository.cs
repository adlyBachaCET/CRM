using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class RequestDoctorRepository : Repository<RequestDoctor>, IRequestDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public RequestDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<RequestDoctor>> GetAllActif()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<RequestDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 0 && a.IdRequestDoctor == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<RequestDoctor>> GetByIdActifDoctor(int id)
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 0 && a.IdDoctor == id).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RequestDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestDoctor>> GetAllPending()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RequestDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequestDoctor>> GetByIdDoctor(int id)
        {
            var result = await MyDbContext.RequestDoctors.Where(a => a.Active == 0 &&  a.IdDoctor==id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<RequestDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyRequestDoctorDbContext.RequestDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
