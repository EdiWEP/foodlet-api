using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
       
        public IngredientRepository(AppDbContext dbcontext) : base(dbcontext) { }

        public void CreateRangeFromList(List<Ingredient> ingredients)
        {
            _set.AddRange(ingredients);
        }

        public async Task<List<Ingredient>> GetAllDefault()
        {
            return await _set.Where(x => x.UserId == null).ToListAsync();
        }


        public async Task<List<Ingredient>> GetByUserId(string userId)
        {
            return await _set.Where(x => x.UserId == null || x.UserId == userId).ToListAsync();
        }

        public async Task<Ingredient> GetByName(string name)
        {
            return await _set.Where(x => x.Name == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<Ingredient>> GetAllOfUser(string userId)
        {
            return await _set.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Ingredient>> SearchByName(string term, string userId)
        {

            return await _set.Where(x => x.Name.Contains(term) && (x.UserId == null || x.UserId == userId)).ToListAsync();
        }
    }
}
