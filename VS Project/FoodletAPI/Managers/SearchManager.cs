using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Interfaces.Repositories;
using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Managers
{
    public class SearchManager : ISearchManager 
    {

        private readonly IIngredientRepository _ingRepo;
        private readonly IRecipeRepository _recipeRepo;
        private readonly IUserRepository _userRepo;


        public SearchManager(IIngredientRepository ingredRepo, IRecipeRepository recipeRepo, IUserRepository userRepo)
        {
            _ingRepo = ingredRepo;
            _recipeRepo = recipeRepo;
            _userRepo = userRepo;
        }

        public async Task<SearchResultModel> SearchIngredientsByName(string query, string userId)
        {
            var returnModel = new SearchResultModel();

            string[] searchTerms = query.Split(' ');

            var searchResults = new List<IngredientWithIdModel>();

            
            foreach(var term in searchTerms)
            {
                var result = await _ingRepo.SearchByName(term, userId);
                
                foreach(var entity in result)
                {
                    searchResults.Add(new IngredientWithIdModel(entity));
                }
            }

            returnModel.Result = searchResults.Distinct().ToList();
            
            if(searchResults.Count == 0)
            {
                returnModel.Code = 204;
            }
            else
            {
                returnModel.Code = 200;
            }

            return returnModel;
        }

        public async Task<SearchResultModel> SearchRecipesByName(string query, string userId)
        {
            var returnModel = new SearchResultModel();

            string[] searchTerms = query.Split(' ');

            var searchResults = new List<ReturnRecipeModel>();

            foreach (var term in searchTerms)
            {
                var result = await _recipeRepo.SearchByName(term, userId);

                foreach (var entity in result)
                {
                    searchResults.Add(new ReturnRecipeModel(entity));
                }
            }

            returnModel.Result = searchResults.Distinct().ToList();

            if (searchResults.Count == 0)
            {
                returnModel.Code = 204;
            }
            else
            {
                returnModel.Code = 200;
            }

            return returnModel;
        }


    }
}
