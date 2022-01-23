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
    public class SearchController : ControllerBase
    {
        private readonly ISearchManager _searchManager;
        private readonly ITokenManager _tokenManager;

        public SearchController(ISearchManager searchManager, ITokenManager tokenManager)
        {
            _searchManager = searchManager;
            _tokenManager = tokenManager;
        }

        [HttpGet("{category}/{query}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Search([FromRoute] string category, [FromRoute] string query, [FromQuery] string user, [FromHeader] string Authorization)
        {

            var requestedSameUser = await _tokenManager.VerifyRequestedUser(Authorization, user, true); 
            if (!requestedSameUser)
            {
                return StatusCode(403);
            }

            SearchResultModel searchResult;

            switch (category)
            {
                case "ingredient":
                    searchResult = await _searchManager.SearchIngredientsByName(query, user);
                    break;
                case "recipe":
                    searchResult  = await _searchManager.SearchRecipesByName(query, user);
                    break;
                default:
                    return BadRequest("Invalid search category");
                    
            }

            return searchResult.Code switch
            {
                200 => Ok(searchResult.Result),
                204 => NoContent(),
                404 => NotFound("User not found"),
                _ => BadRequest()
            };
        }
    }
}
