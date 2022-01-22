using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    
    public class IngredientModel
    {
        public string Name { get; set; }
        public float Calsperg { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public float Protein { get; set; }
    }
}
