﻿using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class EstablishmentRepository : Repository<Establishment>, IEstablishmentRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public EstablishmentRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Establishment>> GetAllActif()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Establishment>> GetAllInActif()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
      
        public async Task<Establishment> GetByIdActif(int id)
        {
            var result = await MyDbContext.Establishment.Where(a => a.Active == 0 && a.IdEstablishment == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Establishment>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Establishment>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Establishment>> GetAllPending()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Establishment>> GetAllRejected()
        {
            var result = await MyDbContext.Establishment.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Establishment>> GetAllWithArtisteAsync()
        //{
        //    return await MyEstablishmentDbContext.Establishments
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
