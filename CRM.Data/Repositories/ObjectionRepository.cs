using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class ObjectionRepository : Repository<Objection>, IObjectionRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public ObjectionRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Objection>> GetAll(Status? Status, RequestObjection RequestObjection)
        {

            if (Status != null) return await MyDbContext.Objection.Where(i => i.Status == Status && i.Active == 0
            && i.RequestObjection == RequestObjection).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();

            return await MyDbContext.Objection.Where(i => i.Active == 0
            && i.RequestObjection == RequestObjection).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();


        }
        public async Task<IEnumerable<Objection>> GetAllByReport(Status? Status, int IdReport)
        {

            if (Status != null) return await MyDbContext.Objection.Where(i => i.Status == Status && i.Active == 0
            && i.IdVisitReport == IdReport).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();

            return await MyDbContext.Objection.Where(i => i.Active == 0
            && i.IdVisitReport == IdReport).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();


        }
        public async Task<IEnumerable<Objection>> GetAll(Status? Status)
        {

            if (Status != null) return await MyDbContext.Objection.Where(i => i.Status == Status && i.Active == 0)
                    .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();

            return await MyDbContext.Objection.Where(i => i.Active == 0)
                .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();


        }
        public async Task<Objection> GetById(int Id, Status? Status, RequestObjection RequestObjection)
        {

            if (Status != null) return await MyDbContext.Objection.Where(i => i.Id == Id && i.Status == Status && i.Active == 0
            && i.RequestObjection == RequestObjection).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).FirstOrDefaultAsync();

            return await MyDbContext.Objection.Where(i => i.Id == Id && i.Active == 0).Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).FirstOrDefaultAsync();


        }
        public async Task<IEnumerable<Objection>> GetAllActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0)
                .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllInActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 1)
                .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product).ToListAsync();
            return result;
        }
        public async Task<Objection> GetByIdActif(int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.Id == id)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetByIdActifDoctor(int Id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.IdDoctor == Id)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetAllPending()
        {
            var result = await MyDbContext.Objection.Where(a => a.Status == Status.Pending)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetAllRejected()
        {
            var result = await MyDbContext.Objection.Where(a => a.Status == Status.Rejected)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetByIdDoctor(RequestObjection RequestObjection,int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 &&  a.IdDoctor==id && a.RequestObjection== RequestObjection)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Objection>> GetByIdPharmacy(RequestObjection RequestObjection, int id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.IdPharmacy == id && a.RequestObjection == RequestObjection)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Objection>> GetByIdActifUser(int Id)
        {
            var result = await MyDbContext.Objection.Where(a => a.Active == 0 && a.IdUser == Id)
                 .Include(i => i.User)
                .Include(i => i.Doctor)
                .Include(i => i.Pharmacy)
                .Include(i => i.Product)
                .ToListAsync();
            return result;
        }
   
    }
}
