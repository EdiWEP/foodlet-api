using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Managers;
using FoodletAPI.Models;
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

        public IngredientController(IIngredientManager ingredientManager)
        {
            _manager = ingredientManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _manager.GetAll();

            return Ok(ingredients); 
        }

        [HttpGet("id/{id}")]
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

        [HttpPost("add")]
//        [SwaggerResponse(200, Type = typeof(IngredientModel))]
        public async Task<IActionResult> AddIngredient([FromBody] IngredientModel newModel)
        {

            if(await _manager.AddIngredient(newModel))
            {

                return Ok("Successfuly added the new ingredient");
            }
            else
            {
                return BadRequest("Couldn't add ingredient");
            }

        }

        [HttpPost("addmany")]
        public async Task<IActionResult> AddListOfIngredients([FromBody] List<IngredientModel> list)
        {
            return Ok(list);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientWithIdModel updatedIngredient)
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
        public async Task<IActionResult> DeleteIngredient([FromRoute] string id)
        {
            var result = await _manager.Delete(id);

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
