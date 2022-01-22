using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class RecipeIngredientWithNameModel : RecipeIngredientModel
    {
        public string Name { get; set; }

        public RecipeIngredientWithNameModel(RecipeIngredient ri) : base(ri)
        {
            Name = ri.Ingredient.Name;
        } 
    }
}
