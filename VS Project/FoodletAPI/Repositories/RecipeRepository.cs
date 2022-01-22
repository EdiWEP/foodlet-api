using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodletAPI.Helpers;

namespace FoodletAPI.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {

        public RecipeRepository(AppDbContext dbcontext) : base(dbcontext) { }

        public async Task<Recipe> GetByIdWithIngredients(string id)
        {
            return await _db.Recipes.WithIngredients().AsNoTracking().FirstOrDefaultAsync<Recipe>(x => x.Id == id);//.Single(x => x.Id == id);
        }

        public async Task<List<Recipe>> GetFromUser(string id)
        {
            return await _db.Recipes.OfUser(id).AsNoTracking().ToListAsync();
        }

        public async Task<List<Recipe>> GetFromUserWithIngredients(string id)
        {
            return await _db.Recipes.WithIngredients().OfUser(id).AsNoTracking().ToListAsync();
        } 

        public async Task<List<Recipe>> GetAllWithIngredients()
        {
            return await _db.Recipes.WithIngredients().AsNoTracking().ToListAsync();
        }

        public void AddRecipeIngredients(List<RecipeIngredient> newIngredients)
        {
            _db.RecipeIngredients.AddRange(newIngredients);
        }

        public void UpdateRecipeIngredients(string recipeId, List<RecipeIngredient> newIngredients)
        {
            _db.RecipeIngredients.RemoveRange(_db.RecipeIngredients.Where(x => x.RecipeId == recipeId));
            _db.RecipeIngredients.AddRange(newIngredients);
        }

        public void DeleteRecipeIngredients(string recipeId)
        {
            _db.RecipeIngredients.RemoveRange(_db.RecipeIngredients.Where(x => x.RecipeId == recipeId));
        }
    }
}
