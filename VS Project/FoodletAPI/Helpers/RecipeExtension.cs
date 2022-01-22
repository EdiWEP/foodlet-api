using FoodletAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Helpers
{
    public static class RecipeExtension
    {
        // Extension method for including Recipe Ingredients
        public static IQueryable<Recipe> WithIngredients(this IQueryable<Recipe> recipes)
        {
            return recipes.Include(x => x.RecipeIngredients).ThenInclude(ri => ri.Ingredient);
        }


        public static IQueryable<Recipe> OfUser(this IQueryable<Recipe> recipes, string id)
        {
            return recipes.Where(x => x.UserId == id);
        }

    }
}
