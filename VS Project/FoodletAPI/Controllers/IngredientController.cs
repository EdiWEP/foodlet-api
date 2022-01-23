using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FoodletAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {

        private readonly IIngredientManager _manager;
        private readonly ITokenManager _tokenManager;

        public IngredientController(IIngredientManager ingredientManager, ITokenManager tokenManager)
        {
            _manager = ingredientManager;
            _tokenManager = tokenManager;
        }

        [HttpGet("all/default")]
        public async Task<IActionResult> GetAllDefaultIngredients()
        {
            var ingredients = await _manager.GetAllDefault();

            return Ok(ingredients);
        }

        [HttpGet("all")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetAllForUser([FromHeader] string Authorization)
        {

            var userId = _tokenManager.GetUserIdFromToken(Authorization);

            var ingredients = await _manager.GetByUserId(userId);

            return Ok(ingredients);
        }

        
        [HttpGet("all/admin")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _manager.GetAll();

            return Ok(ingredients); 
        }


        [HttpGet("all/owned")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetAllOfUser([FromHeader] string Authorization)
        {
            var userId = _tokenManager.GetUserIdFromToken(Authorization);

            var ingredients = await _manager.GetAllOfUser(userId);

            return Ok(ingredients);
        }


        [HttpGet("id/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetIngredientById([FromRoute] string id) 
        { 
        
            var ingredient = await _manager.GetById(id);
            
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            else
            {
                return Ok(ingredient);
            }
        }

        [HttpGet("name/{name}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetIngredientByName([FromRoute] string name)
        {
            var ingredient = await _manager.GetByName(name);

            if(!name.All(c => Char.IsLetter(c) || c == ' '))
            {
                return BadRequest("Name must contain only letters and spaces");
            }

            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            else
            {
                return Ok(ingredient);
            }
        }

        [HttpPost("add")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> AddUserIngredient([FromBody] IngredientModel model, [FromHeader] string Authorization)
        {
            if (!await _tokenManager.VerifyRequestedUser(Authorization, model.UserId))
            {
                return StatusCode(403);
            }

            if (await _manager.AddIngredient(model))
            {
                return Ok("Successfuly added the new ingredient");
            }
            else
            {
                return BadRequest("Couldn't add ingredient");
            }
        }

        [HttpPost("add/admin")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddIngredient([FromBody] IngredientModel model)
        {

            if(await _manager.AddIngredient(model))
            {

                return Ok("Successfuly added the new ingredient");
            }
            else
            {
                return BadRequest("Couldn't add ingredient");
            }

        }

        [HttpPost("addmany")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddListOfIngredients([FromBody] List<IngredientModel> list)
        {

            if (await _manager.AddListOfIngredients(list))
            {
                return Ok("Successfuly added the new ingredients");
            }
            else
            {
                return BadRequest("Couldn't add ingredients");
            }

        }

        [HttpPut("update")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientWithIdModel updatedIngredient, [FromHeader] string Authorization)
        {
            if (!await _tokenManager.VerifyRequestedUser(Authorization, updatedIngredient.UserId))
            {
                return StatusCode(403);
            }

            var result = await _manager.Update(updatedIngredient);

            return result switch
            {
                200 => Ok("Successfully updated the ingredient"),
                404 => NotFound("Couldn't find ingredient by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update ingredient")
            };
        }

        [HttpPut("update/admin")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ForceUpdateIngredient([FromBody] IngredientWithIdModel updatedIngredient)
        {
            var result = await _manager.Update(updatedIngredient);

            return result switch
            {
                200 => Ok("Successfully updated the ingredient"),
                404 => NotFound("Couldn't find ingredient by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update ingredient")
            };
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeleteIngredient([FromRoute] string id, [FromHeader] string Authorization)
        {

            var userId = _tokenManager.GetUserIdFromToken(Authorization);

            var result = await _manager.Delete(id, userId);

            return result switch
            {
                200 => Ok("Successfully updated the ingredient"),
                403 => StatusCode(403),
                404 => NotFound("Couldn't find ingredient by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update ingredient")
            };
        }

        [HttpDelete("delete/{id}/admin")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ForceDeleteIngredient([FromRoute] string id)
        {
            var result = await _manager.ForceDelete(id);

            return result switch
            {
                200 => Ok("Successfully updated the ingredient"),
                404 => NotFound("Couldn't find ingredient by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update ingredient")
            };

        }

    }
}
