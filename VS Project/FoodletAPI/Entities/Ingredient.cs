using FoodletAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities
{
    public class Ingredient
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public float Calsperg { get; set; } 
        public float Fat { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }

        public virtual ICollection<RecipeIngredient> Recipes { get; set; }

        public Ingredient()
        {
            Id = Guid.NewGuid().ToString();
            Name = "defaultName";
            Calsperg = 0.0f;
            Fat = 0.0f;
            Carbs = 0.0f;
            Protein = 0.0f;
            Recipes = null;
        } 
        public Ingredient(string name, float cals, float fat, float carbs, float protein) 
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Calsperg = cals;
            Fat = fat;
            Carbs = carbs;
            Protein = protein;
            Recipes = null;
        }

        public Ingredient(IngredientModel addModel)
        {
            Id = Guid.NewGuid().ToString();
            Name = addModel.Name;
            Calsperg = addModel.Calsperg;
            Fat = addModel.Fat;
            Carbs = addModel.Carbs;
            Protein = addModel.Protein;
            Recipes = null;
        }

        public Ingredient(IngredientWithIdModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Calsperg = model.Calsperg;
            Fat = model.Fat;
            Carbs = model.Carbs;
            Protein = model.Protein;
            Recipes = null;
        }

        public void UpdateFromModel(IngredientModel model)
        {
            Name = model.Name;
            Calsperg = model.Calsperg;
            Fat = model.Fat;
            Carbs = model.Carbs;
            Protein = model.Protein;
        }


        public void UpdateFromModel(IngredientWithIdModel model)
        {
            Name = model.Name;
            Calsperg = model.Calsperg;
            Fat = model.Fat;
            Carbs = model.Carbs;
            Protein = model.Protein;
        }

    }
}
