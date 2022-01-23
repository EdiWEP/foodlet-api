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
    public class RecipeController : ControllerBase
    {

        private readonly IRecipeManager _manager;
        private readonly ITokenManager _tokenManager;
        public RecipeController(IRecipeManager recipeManager, ITokenManager tokenManager)
        {
            _manager = recipeManager;
            _tokenManager = tokenManager;
        }

        [HttpGet("all")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _manager.GetAll();

            return Ok(recipes);
        }

        [HttpGet("all/{userId}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetAllFromUser([FromRoute] string userId, [FromHeader] string Authorization)
        {

            if (!await _tokenManager.VerifyRequestedUser(Authorization, userId))
            {
                return StatusCode(403);
            }

            var recipes = await _manager.GetAllFromUser(userId);

            return Ok(recipes);
        }

        [HttpGet("id/{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetRecipeById([FromRoute] string id)
        {

            var recipe = await _manager.GetById(id);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }
            else
            {
                return Ok(recipe);
            }

        }

        [HttpPost("add")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> AddRecipe([FromBody] AddRecipeModel addModel, [FromHeader] string Authorization)
        {
            
            if(! await _tokenManager.VerifyRequestedUser(Authorization, addModel.UserId))
            {
                return StatusCode(403);
            }

            if(await _manager.AddRecipe(addModel))
            {
                return Ok("Successfully added the new recipe");
            }
            else
            {
                return BadRequest("Couldn't add recipe");
            }
        }

        [HttpPut("update")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeModel updateModel, [FromHeader] string Authorization)
        {
            
            if(! await _tokenManager.VerifyRequestedUser(Authorization, updateModel.UserId))
            {
                return StatusCode(403);
            }

            var result = await _manager.Update(updateModel);

            return result switch
            {
                200 => Ok("Successfully updated the recipe"),
                404 => NotFound("Couldn't find recipe by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't update recipe")
            };
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeleteRecipe([FromRoute] string id, [FromHeader] string Authorization)
        {
            var userId = await _manager.GetUserId(id);

            if (userId == null) return NotFound("Couldn't find recipe by Id");

            if (!await _tokenManager.VerifyRequestedUser(Authorization, userId))
            {
                return StatusCode(403);
            }

            var result = await _manager.Delete(id);

            return result switch
            {
                200 => Ok("Successfully deleted the recipe"),
                404 => NotFound("Couldn't find recipe by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't delete recipe")
            };
        }
    }
}
