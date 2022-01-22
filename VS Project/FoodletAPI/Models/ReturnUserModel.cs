using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class ReturnUserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public ReturnUserModel(User entity)
        {
            Id = entity.Id;
            Username = entity.UserName;
            Email = entity.Email;
        }

    }
}
