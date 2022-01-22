using FoodletAPI.Models;
using FoodletAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface IAccountManager
    {
        Task<List<ReturnUserModel>> GetAllUsers();

        Task<UserWithRolesModel> GetUserWithRoles(string id);

        Task<ReturnUserModel> GetUserById(string id);

        Task<UserProfileModel> GetProfileByUserId(string userId);

        Task CreateDefaultProfile(string userId, string userName);

        Task<int> DeleteUser(string id);

        Task<int> UpdateProfile(UpdateUserProfileModel model);
    }
}
