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
        Task<List<IngredientWithIdModel>> GetAll();

        Task<IngredientWithIdModel> GetById(string id);

        Task<bool> AddIngredient(IngredientModel addModel);

        Task<int> Delete(string id);

        Task<int> Update(IngredientWithIdModel ingredientModel);

    }
}
