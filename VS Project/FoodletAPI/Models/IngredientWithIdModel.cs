using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class IngredientWithIdModel : IngredientModel
    {
        public string Id { get; set; }

        public IngredientWithIdModel(Ingredient entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Calsperg = entity.Calsperg;
            this.Protein = entity.Protein;
            this.Fat = entity.Fat;
            this.Carbs = entity.Carbs;
    
        }
    }
}
