using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodletAPI.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenManager _tokenManager;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenManager tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
        }

        public async Task Register(RegisterModel registerModel)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.Username
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);//.CreateAsync(user, signupUserModel.Password);

            await _userManager.AddToRoleAsync(user, "USER");
        }

        public async Task RegisterAdmin(RegisterModel registerModel)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.Username
                
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            await _userManager.AddToRoleAsync(user, "ADMIN");

        }
        public async Task<TokenModel> Login(LoginModel loginModel)
        {
            User user;
            if (loginModel.Email != null)
            {
                user = await _userManager.FindByEmailAsync(loginModel.Email);
            }
            else {
                user = await _userManager.FindByNameAsync(loginModel.Username);
            }
            
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
                if (result.Succeeded)
                {
                    var token = await _tokenManager.CreateToken(user);
                    return new TokenModel { Token = token };
                }
            }

            return null;
        }

    }
}
