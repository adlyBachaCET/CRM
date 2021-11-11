
using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class TagsDoctorRepository : Repository<TagsDoctor>, ITagsDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public TagsDoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TagsDoctor>> GetAllActif()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllInActif()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<TagsDoctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Active == 0 && a.IdTags == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllPending()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<TagsDoctor>> GetAllRejected()
        {
            var result = await MyDbContext.TagsDoctor.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<TagsDoctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyTagsDoctorDbContext.TagsDoctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
