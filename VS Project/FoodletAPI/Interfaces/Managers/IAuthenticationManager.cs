using FoodletAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface IAuthenticationManager
    {
        Task<int> Register(RegisterModel registerModel);
        Task<int> RegisterAdmin(RegisterModel registerModel);
        Task<TokenModel> Login(LoginModel loginModel);
    }
}
