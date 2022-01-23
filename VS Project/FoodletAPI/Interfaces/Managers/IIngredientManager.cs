using FoodletAPI.Entities;
using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface IIngredientManager
    {
        Task<bool> AddListOfIngredients(List<IngredientModel> ingredientModels);

        Task<List<IngredientWithIdModel>> GetAll();

        Task<IngredientWithIdModel> GetById(string id);

        Task<IngredientWithIdModel> GetByName(string name);

        Task<List<IngredientWithIdModel>> GetAllOfUser(string userId);
        Task<List<IngredientWithIdModel>> GetByUserId(string userId);
        Task<List<IngredientWithIdModel>> GetAllDefault();

        Task<bool> AddIngredient(IngredientModel addModel);

        Task<int> Delete(string id);

        Task<int> Update(IngredientWithIdModel ingredientModel);

    }
}
