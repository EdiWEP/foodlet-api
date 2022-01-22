using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class AddRecipeModel
    {

        public string UserId { get; set; }
        public string Name { get; set; }

        public float Calsperg { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public float Protein { get; set; }
        public int NumberOfIngredients { get; set; }
        public int ServingSize { get; set; }

        public List<RecipeIngredientModel> Ingredients { get; set; }

    }
}
