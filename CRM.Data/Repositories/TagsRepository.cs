using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class TagsRepository : Repository<Tags>, ITagsRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public TagsRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<Tags> GetByExistantActif(string Name)
        {
            var result = await MyDbContext.Tags.Where(a => a.Name == Name && a.Active == 0).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Tags>> GetAllActif()
        {
            var result = await MyDbContext.Tags.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Tags>> GetAllInActif()
        {
            var result = await MyDbContext.Tags.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Tags> GetByIdActif(int id)
        {
            var result = await MyDbContext.Tags.Where(a => a.Active == 0 && a.IdTags == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Tags>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Tags.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Tags>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Tags.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Tags>> GetAllPending()
        {
            var result = await MyDbContext.Tags.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Tags>> GetAllRejected()
        {
            var result = await MyDbContext.Tags.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Tags>> GetAllWithArtisteAsync()
        //{
        //    return await MyTagsDbContext.Tagss
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
