using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models;
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

        public RecipeController(IRecipeManager recipeManager)
        {
            _manager = recipeManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _manager.GetAll();

            return Ok(recipes);
        }

        [HttpGet("id/{id}")]
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
        public async Task<IActionResult> AddRecipe([FromBody] AddRecipeModel addModel)
        {
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
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeModel updateModel)
        {
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
        public async Task<IActionResult> DeleteRecipe([FromRoute] string id)
        {
            var result = await _manager.Delete(id);

            return result switch
            {
                200 => Ok("Successfully deleted the ingredient"),
                404 => NotFound("Couldn't find ingredient by Id"),
                500 => StatusCode(500),
                _ => BadRequest("Couldn't delete ingredient")
            };
        }
    }
}
