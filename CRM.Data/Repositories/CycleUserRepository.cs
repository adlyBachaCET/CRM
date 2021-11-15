﻿using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class CycleUserRepository : Repository<CycleUser>, ICycleUserRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public CycleUserRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CycleUser>> GetAllActif()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleUser>> GetAllInActif()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<CycleUser> GetByIdActif(int id)
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Active == 0 && a.IdCycle == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<CycleUser>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleUser>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CycleUser>> GetAllPending()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<CycleUser>> GetAllRejected()
        {
            var result = await MyDbContext.CycleUser.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<CycleUser>> GetAllWithArtisteAsync()
        //{
        //    return await MyCycleUserDbContext.CycleUsers
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
