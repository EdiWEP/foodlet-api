using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models.Auth
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserProfileModel UserProfile { get; set; }
    }
}
