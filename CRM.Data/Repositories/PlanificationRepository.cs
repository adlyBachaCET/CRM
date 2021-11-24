using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PlanificationRepository : Repository<Planification>, IPlanificationRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PlanificationRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Planification>> GetAllActif()
        {
            var result = await MyDbContext.Planification.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Planification>> GetAllInActif()
        {
            var result = await MyDbContext.Planification.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Planification> GetByIdActif(int id)
        {
            var result = await MyDbContext.Planification.Where(a => a.Active == 0 && a.IdPlanification == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Planification>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Planification.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Planification>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Planification.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Planification>> GetAllPending()
        {
            var result = await MyDbContext.Planification.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Planification>> GetAllRejected()
        {
            var result = await MyDbContext.Planification.Where(a => a.Status == Status.Rejected).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Planification>> GetAllById(int Id)
        {
            var result = await MyDbContext.Planification.Where(a =>a.Active==0 && a.IdPlanification==Id).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Planification>> GetAllWithArtisteAsync()
        //{
        //    return await MyPlanificationDbContext.Planifications
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
