using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Repositories
{
    public interface IProfileRepository : IBaseRepository<UserProfile>
    {
        Task<UserProfile> GetByUserName(string username);
        Task<UserProfile> GetByUserId(string userId);
    }
}
