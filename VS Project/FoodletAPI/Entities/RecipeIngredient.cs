using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities
{
    public class RecipeIngredient
    {
        public string Id { get; set; }
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }
        public int Grams { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }


        public RecipeIngredient()
        {
            Id = Guid.NewGuid().ToString();
            RecipeId = null;
            IngredientId = null;
            Grams = 0;
            Ingredient = null;
            Recipe = null;
        }
        public RecipeIngredient(string recipeId, RecipeIngredientModel model)
        {
            Id = Guid.NewGuid().ToString();
            RecipeId = recipeId;
            IngredientId = model.IngredientId;
            Grams = model.Grams;
        }
    
    }
}
