using FoodletAPI.Helpers;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationManager _manager;
        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _manager = authenticationManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            var result = await _manager.Register(model);

            return result switch
            {
                AuthConstants.OK => Ok("Successfully registered"),
                AuthConstants.NULL_USERNAME => BadRequest("Missing username"),
                AuthConstants.BAD_USERNAME => BadRequest("Invalid username"),
                AuthConstants.TAKEN_USERNAME => BadRequest("Username is already taken"),
                AuthConstants.NULL_EMAIL => BadRequest("Missing email"),
                AuthConstants.BAD_EMAIL=> BadRequest("Invalid email"),
                AuthConstants.TAKEN_EMAIL => BadRequest("Email is already taken"),
                AuthConstants.SHORT_PWD => BadRequest("Password must contain at least 8 characters"),
                AuthConstants.PWD_NODIGIT => BadRequest("Password must contain at least one digit"),
                _ => BadRequest("Unknown error occurred")
            };
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _manager.Login(model);
            
            if (result == null)
            {
                return BadRequest("Invalid credentials");
            }
            else { 
                return Ok(await _manager.Login(model));
            }
        }

        [HttpPost("newadmin")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _manager.RegisterAdmin(model);

            return result switch
            {
                AuthConstants.OK => Ok("Successfully registered"),
                AuthConstants.NULL_USERNAME => BadRequest("Missing username"),
                AuthConstants.BAD_USERNAME => BadRequest("Invalid username"),
                AuthConstants.TAKEN_USERNAME => BadRequest("Username is already taken"),
                AuthConstants.NULL_EMAIL => BadRequest("Missing email"),
                AuthConstants.BAD_EMAIL => BadRequest("Invalid email"),
                AuthConstants.TAKEN_EMAIL => BadRequest("Email is already taken"),
                AuthConstants.SHORT_PWD => BadRequest("Password must contain at least 8 characters"),
                AuthConstants.PWD_NODIGIT => BadRequest("Password must contain at least one digit"),
                _ => BadRequest("Unknown error occurred")
            };

        }
    }
}
