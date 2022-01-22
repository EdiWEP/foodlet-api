using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Repositories;
using FoodletAPI.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Repositories
{
    public class ProfileRepository : BaseRepository<UserProfile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext dbcontext) : base(dbcontext) { }

        public async Task<UserProfile> GetByUserId(string userId)
        {
            return await _db.UserProfiles.Where(up => up.UserId == userId).FirstOrDefaultAsync();
         
        } 
    }
}
