using FoodletAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities
{
    public class User : IdentityUser<string>
    {
        public virtual UserProfile Profile { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public User() : base()
        {

        }

        public User(RegisterModel registerModel) : base()
        {
            
            UserName = registerModel.Username;
            Email = registerModel.Email;

        }
    }
}
