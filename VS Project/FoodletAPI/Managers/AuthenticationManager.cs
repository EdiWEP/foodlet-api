using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FoodletAPI.Helpers;

namespace FoodletAPI.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenManager _tokenManager;
        private readonly IAccountManager _accountManager;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenManager tokenManager, IAccountManager accountManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
            _accountManager = accountManager;
        }

        public async Task<int> Register(RegisterModel registerModel)
        {
            // Trim bad characters
            registerModel.Email = TrimInput(registerModel.Email);
            
            int verifyResult;

            verifyResult = await VerifyEmail(registerModel.Email);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;

            verifyResult = await VerifyUsername(registerModel.Username);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;

            verifyResult = VerifyPassword(registerModel.Password);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;


            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.Username,

            };

            await _userManager.CreateAsync(user, registerModel.Password);
            await _userManager.AddToRoleAsync(user, "USER");
            _accountManager.CreateDefaultProfile(user.Id, user.UserName);
            

            return AuthConstants.OK;
        }

        public async Task<int> RegisterAdmin(RegisterModel registerModel)
        {
            registerModel.Email = TrimInput(registerModel.Email);
            
            int verifyResult;

            verifyResult = await VerifyEmail(registerModel.Email);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;

            verifyResult = await VerifyUsername(registerModel.Username);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;

            verifyResult = VerifyPassword(registerModel.Password);
            if (!verifyResult.Equals(AuthConstants.OK)) return verifyResult;


            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.Username
                
            };

            await _userManager.CreateAsync(user, registerModel.Password);
            await _userManager.AddToRoleAsync(user, "ADMIN");
            _accountManager.CreateDefaultProfile(user.Id, user.UserName);

            return AuthConstants.OK;
        }

        public async Task<TokenModel> Login(LoginModel loginModel)
        {
            
            
            User user;
            
            if (loginModel.Email != null)
            {
                loginModel.Email = TrimInput(loginModel.Email);
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

        private async Task<int> VerifyEmail(string email)
        {
            if(email == null)
            {
                return AuthConstants.NULL_EMAIL;
            }

            if (! new EmailAddressAttribute().IsValid(email) )
            {
                return AuthConstants.BAD_EMAIL;
            }

            if(await _userManager.FindByEmailAsync(email) != null)
            {
                return AuthConstants.TAKEN_EMAIL;
            }

            return AuthConstants.OK;
        }

        private async Task<int> VerifyUsername(string username)
        {
            if(username == null)
            {
                return AuthConstants.NULL_USERNAME;
            }

            string validCharacters = AuthConstants.VALID_USER_CHARS;

            foreach(char c in username)
            {
                if (!validCharacters.Contains(c))
                {
                    // Username contains illegal character
                    return AuthConstants.BAD_USERNAME;
                }
            }

            if (await _userManager.FindByNameAsync(username) != null)
            {
                return AuthConstants.TAKEN_USERNAME;
            }

            return AuthConstants.OK;
        }

        private int VerifyPassword(string password)
        {
            if(password.Length < 8)
            {
                // Password too short
                return AuthConstants.SHORT_PWD;
            }

            if(!password.Any(char.IsDigit)) {
                // Password doesn't contain a digit
                return AuthConstants.PWD_NODIGIT;
            }

            return AuthConstants.OK;
        }

        private string TrimInput(string input)
        {
            return input.Trim(AuthConstants.INPUT_TRIM_CHARS);
        }
    }
}
