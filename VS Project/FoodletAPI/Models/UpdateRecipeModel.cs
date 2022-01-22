using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class UpdateRecipeModel : AddRecipeModel
    {
        public string Id { get; set; }
    }
}
