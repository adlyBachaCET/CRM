using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        public Repository(MyDbContext context)
        {
            _context = context;
        }
        //public async Task IRepository<TEntity>.AddAsync(TEntity entity)
        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity record)
        {
        
            _context.Set<TEntity>().Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        //public async Task IRepository<TEntity>.AddRangeAsync(IEnumerable<TEntity> entities)
        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        //public IEnumerable<TEntity> IRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate)
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        //public async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAllAsync()
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        //public async ValueTask<TEntity> IRepository<TEntity>.GetByIdAsync(int id)
        public async ValueTask<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        //public void IRepository<TEntity>.Remove(TEntity entity)
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        //async ValueTask<TEntity> IRepository<TEntity>.SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        public Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
   

    }
}
