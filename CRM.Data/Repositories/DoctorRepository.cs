using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public DoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Doctor>> GetAllActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllInActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Doctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.IdDoctor == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllPending()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Doctor>> GetAllRejected()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Doctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyDoctorDbContext.Doctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
