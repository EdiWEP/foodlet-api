using FoodletAPI.Entities;
using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface IRecipeManager
    {
        Task<List<ReturnRecipeModel>> GetAll();

        Task<ReturnRecipeModel> GetById(string id);

        Task<bool> AddRecipe(AddRecipeModel addModel);

        Task<int> Delete(string id);

        Task<int> Update(UpdateRecipeModel recipeModel);

    }
}
