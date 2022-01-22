using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Repositories
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<Recipe> GetByIdWithIngredients(string id);

        Task<List<Recipe>> GetFromUser(string id);

        Task<List<Recipe>> GetFromUserWithIngredients(string id);

        Task<List<Recipe>> GetAllWithIngredients();

        void AddRecipeIngredients(List<RecipeIngredient> newIngredients);

        void UpdateRecipeIngredients(string recipeId, List<RecipeIngredient> newIngredients);

        void DeleteRecipeIngredients(string recipeId);

    }
}
