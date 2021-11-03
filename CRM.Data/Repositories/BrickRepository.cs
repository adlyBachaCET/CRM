using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class BrickRepository : Repository<Brick>, IBrickRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public BrickRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Brick>> GetAllActif()
        {
            var result = await MyDbContext.Brick.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Brick>> GetAllInActif()
        {
            var result = await MyDbContext.Brick.Where(a => a.Active == 1).ToListAsync();
            return result;
        }

        public async Task<Brick> GetByIdActif(int id)
        {
            var result = await MyDbContext.Brick.Where(a => a.Active == 0 && a.IdBrick== id && a.Status==Status.Approuved).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Brick>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Brick.Where(a => a.Active == 0 && a.Status==Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Brick>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Brick.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Brick>> GetAllPending()
        {
            var result = await MyDbContext.Brick.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Brick>> GetAllRejected()
        {
            var result = await MyDbContext.Brick.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Brick>> GetAllWithArtisteAsync()
        //{
        //    return await MyBrickDbContext.Bricks
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
