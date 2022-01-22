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
    }
}
