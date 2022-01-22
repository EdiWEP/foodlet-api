using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models.Auth;
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
            await _manager.Register(model);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok(await _manager.Login(model));
        }

        [HttpPost("newadmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            await _manager.RegisterAdmin(model);

            return Ok();
        }
    }
}
