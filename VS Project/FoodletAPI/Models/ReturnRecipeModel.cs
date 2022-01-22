using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class ReturnRecipeModel 
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public float Calsperg { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public float Protein { get; set; }
        public int NumberOfIngredients { get; set; }
        public int ServingSize { get; set; }

        public List<RecipeIngredientWithNameModel> Ingredients { get; set; }

        public ReturnRecipeModel(Recipe entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            UserId = entity.UserId;
            Calsperg = entity.Calsperg;
            Carbs = entity.Carbs;
            Fat = entity.Fat;
            Protein = entity.Protein;
            NumberOfIngredients = entity.NumberOfIngredients;
            ServingSize = entity.ServingSize;

            Ingredients = new List<RecipeIngredientWithNameModel>();
            foreach(var ingredient in entity.RecipeIngredients)
            {
                Ingredients.Add(new RecipeIngredientWithNameModel(ingredient));
            }

        }
    }
}
