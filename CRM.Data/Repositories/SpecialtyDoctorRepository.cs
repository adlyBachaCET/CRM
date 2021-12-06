using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SpecialtyDoctorRepository : Repository<SpecialtyDoctor>, ISpecialtyDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SpecialtyDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllActif()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }
        public async Task<SpecialtyDoctor> GetById(int idDoctor, int idSpecialty)
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(i => i.IdDoctor == idDoctor && i.IdSpecialty == idSpecialty && i.Active == 0).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<SpecialtyDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 0 && a.IdSpecialty == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialtyDoctor>> GetAllPending()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SpecialtyDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

    }
}
