using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface ITokenManager
    {
        Task<string> CreateToken(User user);

        // Verifies if the userId in the token matches the requested userId
        Task<bool> VerifyRequestedUser(string tokenHeader, string userId, bool byUsername = false);
    }
}
