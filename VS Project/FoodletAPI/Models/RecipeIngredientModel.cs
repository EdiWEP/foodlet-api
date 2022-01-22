using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class RecipeIngredientModel
    {
        public string IngredientId { get; set; }

        public int Grams { get; set; }

        public RecipeIngredientModel()
        {

        }
        public RecipeIngredientModel(RecipeIngredient ri)
        {
            IngredientId = ri.IngredientId;
            Grams = ri.Grams;
        }
    }
}
