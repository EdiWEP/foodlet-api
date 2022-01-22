using FoodletAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Managers
{
    public interface IAuthenticationManager
    {
        Task Register(RegisterModel registerModel);
        Task RegisterAdmin(RegisterModel registerModel);
        Task<TokenModel> Login(LoginModel loginModel);
    }
}
