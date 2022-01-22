using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces
{
    public interface IBaseRepository<TEntity> 
    {

        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(string id);

        void Create(TEntity newEntry);

        void Create(List<TEntity> entryList);

        void Update(TEntity changedEntity);

        void Delete(TEntity entity);

        Task<bool> SaveChanges();
    }
}
