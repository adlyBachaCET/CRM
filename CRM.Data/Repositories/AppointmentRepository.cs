
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

        public async Task<IEnumerable<Appointement>> GetAllActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllInActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Appointement> GetByIdActif(int id)
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0 && a.IdAppointement == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllPending()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Appointement>> GetAllRejected()
        {
            var result = await MyDbContext.Appointement.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointement>> GetAllById(int Id)
        {
            var result = await MyDbContext.Appointement.Where(a =>a.Active==0 && a.IdAppointement==Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Appointement>> GetAllWithArtisteAsync()
        //{
        //    return await MyAppointementDbContext.Appointements
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
