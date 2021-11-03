using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class SpecialityDoctorRepository : Repository<SpecialityDoctor>, ISpecialityDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public SpecialityDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllActif()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<SpecialityDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Active == 0 && a.IdSpecialty == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialityDoctor>> GetAllPending()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SpecialityDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.SpecialityDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<SpecialityDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MySpecialityDoctorDbContext.SpecialityDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
