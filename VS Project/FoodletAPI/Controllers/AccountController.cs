using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _manager;

        public AccountController(IAccountManager accountManager)
        {
            _manager = accountManager;
        }

        [HttpGet("all")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _manager.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _manager.GetUserById(id);

            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("user/withroles/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUserWithRoles(string id)
        {
            var user = await _manager.GetUserWithRoles(id);

            if(user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("profile/{userId}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetProfileByUserId([FromRoute] string userId)
        {

            var profile = await _manager.GetProfileByUserId(userId);

            if (profile == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(profile);
            }
        }

        [HttpPut("update/profile")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileModel model)
        {
            var result = await _manager.UpdateProfile(model);

            return result switch
            {
                200 => Ok("Successfully updated the profile"),
                404 => NotFound("User not found"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update profile")
            };
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var result = await _manager.DeleteUser(id);

            return result switch
            {
                200 => Ok("Successfully deleted the user"),
                404 => NotFound("Couldn't find user by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't delete ingredient")
            };

        }
    }
}
