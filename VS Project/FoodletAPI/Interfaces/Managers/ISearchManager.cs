using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface ISearchManager
    {

        Task<SearchResultModel> SearchIngredientsByName(string query, string userId);

        Task<SearchResultModel> SearchRecipesByName(string query, string userId);
    }
}
