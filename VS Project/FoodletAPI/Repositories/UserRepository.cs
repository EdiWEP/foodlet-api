using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Repositories;
using FoodletAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbcontext) : base(dbcontext) { }


        public async Task<List<RoleModel>> GetRoleListByUserId(string id)
        {
            
            var roleList = await _db.Users.Where(u => u.Id == id)
                    .Join(_db.UserRoles,
                            u => u.Id, ur => ur.UserId,
                            (u, ur) => new
                            {
                                RoleId = ur.RoleId
                            }
                         ).Join(_db.Roles,
                                x => x.RoleId, r => r.Id,
                                (x, r) => new RoleModel()
                                {
                                    Id = r.Id,
                                    Name = r.Name
                                }
                            ).ToListAsync();
            return roleList;
        } 

        public async Task<string> GetIdByUserName(string username)
        {
            return await _set.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
