using FoodletAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> _set;

        public BaseRepository(AppDbContext dbcontext)
        {
            _db = dbcontext;
            _set = _db.Set<TEntity>();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            var all = await _set.AsNoTracking().ToListAsync();
            
            return all;
        }

        public void Create(TEntity newEntry)
        {
            _set.Add(newEntry);
        }

        public void Create(List<TEntity> entryList)
        {
            foreach(TEntity entity in entryList) {
                _set.Add(entity);
            }
        }

        public void Update(TEntity changedEntity)
        {
            _set.Update(changedEntity);
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }
    
        public async Task<bool> SaveChanges()
        {
            // SaveChangesAsync returns the number of changes made
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
