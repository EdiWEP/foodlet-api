using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Interfaces.Repositories;
using FoodletAPI.Models;
using FoodletAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserRepository _userRepo;
        private readonly IProfileRepository _profileRepo;

        public AccountManager(IUserRepository userRepo, IProfileRepository profileRepo)
        {
            _userRepo = userRepo;
            _profileRepo = profileRepo;
        } 

        public async Task<List<ReturnUserModel>> GetAllUsers()
        {
            var entities = await _userRepo.GetAll();

            var users = new List<ReturnUserModel>();
            foreach(var entity in entities)
            {
                users.Add(new ReturnUserModel(entity));
            }

            return users;
        }

        public async Task<string> GetUserIdByUserName(string username)
        {
            return await _userRepo.GetIdByUserName(username);
        }

        public async Task<ReturnUserModel> GetUserById(string id)
        {
            var entity = await _userRepo.GetById(id);

            var user = new ReturnUserModel(entity);

            return user;
        }

        public async Task<UserWithRolesModel> GetUserWithRoles(string id)
        {
            var user = await _userRepo.GetById(id);

            var roles = await _userRepo.GetRoleListByUserId(id);

            var returnModel = new UserWithRolesModel(user, roles);

            return returnModel;
        }

        public async Task<UserProfileModel> GetProfileByUsername(string username)
        {
            var entity = await _profileRepo.GetByUserName(username);

            var profile = new UserProfileModel(entity);

            return profile;
        }

        public async Task<UserProfileModel> GetProfileByUserId(string userId)
        {
            var entity = await _profileRepo.GetByUserId(userId);

            var profile = new UserProfileModel(entity);

            return profile;
        }

        public async Task CreateDefaultProfile(string userId, string userName)
        {
            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                FullName = userName,
                PhoneNumber = null,
                Description = null
            };

            _profileRepo.Create(newProfile);
            await _profileRepo.SaveChanges();

        }

        public async Task<int> DeleteUser(string id)
        {

            var user = await _userRepo.GetById(id);

            if (user == null)
            {
                return 404;
            }

            var profile = await _profileRepo.GetByUserId(id);
            
            _profileRepo.Delete(profile);
            _userRepo.Delete(user);


            if (await _userRepo.SaveChanges())
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }

        public async Task<int> UpdateProfile(UpdateUserProfileModel model)
        {
            var profile = await _profileRepo.GetByUserId(model.UserId);

            if (profile == null)
            {
                return 404;
            }

            profile.UpdateFromModel(model);

            _profileRepo.Update(profile);

            if (await _profileRepo.SaveChanges())
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }

        
    }
}
