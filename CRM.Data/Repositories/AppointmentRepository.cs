
using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class AppointementRepository : Repository<Appointement>, IAppointementRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public AppointementRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<List<Appointement>> GetByIdUser(int id)
        {
            List<Appointement> Appointements = new List<Appointement>();
            var List = await MyDbContext.Appointement.Where(i => i.IdUser == id && i.Active == 0).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            foreach (var item in List)
            {
                var Appointement = await MyDbContext.Appointement.Where(i => i.IdAppointement == item.IdAppointement && i.Active == 0).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).FirstOrDefaultAsync();

                Appointements.Add(Appointement);
            }
            return
                  Appointements;
        }
        public async Task<IEnumerable<Appointement>> GetAllActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllInActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 1).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }
        public async Task<Appointement> GetByIdActif(int id)
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0 && a.IdAppointement == id).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0 && a.Status == Status.Approuved).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 1 && a.Status == Status.Approuved).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllPending()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Status == Status.Pending).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Appointement>> GetAllRejected()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Status == Status.Rejected).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllById(int Id)
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0 && a.IdAppointement == Id).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            return result;
        }

    }
}
