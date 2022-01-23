using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Interfaces.Repositories;
using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Managers
{
    public class IngredientManager : IIngredientManager
    {
        private readonly IIngredientRepository _repo;

        public IngredientManager(IIngredientRepository ingredRepo)
        {
            _repo = ingredRepo;
        }

        public async Task<List<IngredientWithIdModel>> GetAll()
        {
            var ingredientEntities = await _repo.GetAll();

            var ingredients = new List<IngredientWithIdModel>();

            foreach (var entity in ingredientEntities)
            {
                ingredients.Add(new IngredientWithIdModel(entity));
            }

            return ingredients;
        }

        public async Task<bool> AddIngredient(IngredientModel newModel)
        {
            var newIngredient = new Ingredient(newModel);

            _repo.Create(newIngredient);
            return await _repo.SaveChanges();
        }

        public async Task<bool> AddListOfIngredients(List<IngredientModel> ingredientModels)
        {
            var ingredients = new List<Ingredient>();

            foreach(var model in ingredientModels)
            {
                ingredients.Add(new Ingredient(model));
            }

            _repo.CreateRangeFromList(ingredients);

            return await _repo.SaveChanges();
        }

        public async Task<IngredientWithIdModel> GetById(string id) 
        { 
            var entity = await _repo.GetById(id);

            if (entity == null)
            {
                return null;
            }
            return new IngredientWithIdModel(entity);
        }

        public async Task<IngredientWithIdModel> GetByName(string name)
        {
            var entity = await _repo.GetByName(name);

            if (entity == null)
            {
                return null;
            }
            return new IngredientWithIdModel(entity);
        }

        public async Task<List<IngredientWithIdModel>> GetAllDefault()
        {
            var entities = await _repo.GetAllDefault();

            var ingredients = new List<IngredientWithIdModel>();

            foreach (var entity in entities)
            {
                ingredients.Add(new IngredientWithIdModel(entity));
            }

            return ingredients;
        }

        public async Task<List<IngredientWithIdModel>> GetByUserId(string userId)
        {
            var entities = await _repo.GetByUserId(userId);

            var ingredients = new List<IngredientWithIdModel>();

            foreach (var entity in entities)
            {
                ingredients.Add(new IngredientWithIdModel(entity));
            }

            return ingredients;
        }

        public async Task<List<IngredientWithIdModel>> GetAllOfUser(string userId)
        {
            var entities = await _repo.GetAllOfUser(userId);

            var ingredients = new List<IngredientWithIdModel>();

            foreach (var entity in entities)
            {
                ingredients.Add(new IngredientWithIdModel(entity));
            }

            return ingredients;
        }


        public async Task<int> Update(IngredientWithIdModel ingredientModel) 
        {
            var ingredient = await _repo.GetById(ingredientModel.Id);

            if(ingredient == null)
            {
                return 404;
            }

            ingredient.UpdateFromModel(ingredientModel);

            _repo.Update(ingredient);

            if(await _repo.SaveChanges())
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }

        public async Task<int> Delete(string id)
        {
            var ingredient = await _repo.GetById(id);

            if (ingredient == null)
            {
                return 404;
            }

            _repo.Delete(ingredient);

            if (await _repo.SaveChanges())
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }
    }
}
