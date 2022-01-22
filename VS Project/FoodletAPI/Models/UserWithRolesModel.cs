using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class UserWithRolesModel : ReturnUserModel
    {
        public List<RoleModel> Roles { get; set; }

        public UserWithRolesModel(User entity, List<RoleModel> roles) : base(entity)
        {
            Roles = roles;
        }
    }
}
