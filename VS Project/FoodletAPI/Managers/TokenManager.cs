using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FoodletAPI.Interfaces.Repositories;
using Newtonsoft.Json;

namespace FoodletAPI.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepo;

        public TokenManager(UserManager<User> userManager, IUserRepository userRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
        }


        public async Task<bool> VerifyRequestedUser(string tokenHeader, string user, bool byUsername = false)
        {
            if(byUsername)
            {
                user = await _userRepo.GetIdByUserName(user);
            }
            
            if (user == null)
            {
                return false;
            }
            
            // tokenHeader has the following structure: Bearer <token>
            // Get the token only, then read it and check the NameIdentifier claim which contains the UserId
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenHeader.Split(' ')[1]);
            var claims = token.Payload.Claims.ToList();
            
            foreach(var claim in claims)
            {

                if (claim.Type == "nameid")
                {

                    if (claim.Value == user)
                    {

                        return true;
                    }
                }
            }

            return false;
        }


        public async Task<string> CreateToken(User user)
        {
            var secretKey = Environment.GetEnvironmentVariable("APIKEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
