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
    public class RecipeManager : IRecipeManager
    {

        private readonly IRecipeRepository _repo;

        public RecipeManager(IRecipeRepository recipeRepo)
        {
            _repo = recipeRepo;
        }

        public async Task<List<ReturnRecipeModel>> GetAll()
        {
            var recipeEntities = await _repo.GetAllWithIngredients();

            var recipes = new List<ReturnRecipeModel>();

            foreach (var entity in recipeEntities)
            {
                recipes.Add(new ReturnRecipeModel(entity));
            }

            return recipes;
        }

        public async Task<ReturnRecipeModel> GetById(string id)
        {
            var entity = await _repo.GetByIdWithIngredients(id);

            if (entity != null)
            {
                var recipe = new ReturnRecipeModel(entity);
                return recipe;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ReturnRecipeModel>> GetAllFromUser(string userId)
        {
            var entities = await _repo.GetFromUserWithIngredients(userId);

            var recipes = new List<ReturnRecipeModel>();

            foreach(var entity in entities)
            {
                recipes.Add(new ReturnRecipeModel(entity));
            }

            return recipes;
        }

        public async Task<string> GetUserId(string id)
        {
            var entity = await _repo.GetById(id);

            if (entity == null) return null;
            return entity.UserId;
        }

        public async Task<bool> AddRecipe(AddRecipeModel addModel) 
        {
            var newRecipe = new Recipe(addModel);

            
            var newIngredients = new List<RecipeIngredient>();

            foreach(var ingredient in addModel.Ingredients)
            {
                newIngredients.Add(new RecipeIngredient(newRecipe.Id, ingredient));
            }

            _repo.Create(newRecipe);
            _repo.AddRecipeIngredients(newIngredients);

            return await _repo.SaveChanges();
        }

        public async Task<int> Delete(string id)
        {
            var recipe = await _repo.GetById(id);

            if (recipe == null)
            {
                return 404;
            }

            _repo.DeleteRecipeIngredients(id);
            _repo.Delete(recipe);

            if (await _repo.SaveChanges())
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }

        public async Task<int> Update(UpdateRecipeModel recipeModel)
        {
            var recipe = await _repo.GetById(recipeModel.Id);

            if (recipe == null)
            {
                return 404;
            }

            var newIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in recipeModel.Ingredients)
            {
                newIngredients.Add(new RecipeIngredient(recipeModel.Id, ingredient));
            }

            _repo.UpdateRecipeIngredients(recipeModel.Id, newIngredients);
            
            recipe.UpdateFromModel(recipeModel);
            _repo.Update(recipe);

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
