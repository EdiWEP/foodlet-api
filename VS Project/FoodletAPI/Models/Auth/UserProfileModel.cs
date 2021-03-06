using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models.Auth
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public UserProfileModel(UserProfile profile)
        {
            FullName = profile.FullName;
            PhoneNumber = profile.PhoneNumber;
            Description = profile.Description;
        }
    }
}
