using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TheBerbakery.API.Models;
using TheBerbakery.API.Services.Interfaces;

namespace TheBerbakery.API.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("RecipeCard.API/[controller]")]
    [ApiController]
    //TODO - Decide if successful search with no results should return a 404 or a 200 with an empty array
    public class RecipeCardController : ControllerBase
    {
        #region Service Constructor
        private readonly IRecipeCardService _recipeCardService;
        public RecipeCardController(IRecipeCardService recipeCardService)
        {
            _recipeCardService = recipeCardService;
        }
        #endregion
        /// <summary>
        /// Get List of Specific Recipe Cards
        /// </summary>
        /// <param name="recipeIds"></param>
        /// <returns>List of matching Recipe records and their associated RecipeSteps and RecipeIngredients</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("recipeCards")]
        public async Task<ActionResult<List<Recipe>>> GetRecipeCards([FromQuery]int[] recipeIds, string? recipeName = null)
        {
            if (recipeIds.Length == 0 &&  recipeName == null)
            {
                return BadRequest();
            }
            var recipeCards = await _recipeCardService.GetRecipeCards(recipeIds, recipeName);
            if (recipeCards.Count == 0)
            {
                return NotFound();
            }
            return Ok(recipeCards);
        }

        /// <summary>
        /// Get List of x most recent Recipe Cards
        /// </summary>
        /// <param name="topX"></param>
        /// <returns>List of x most recent Recipe records and their associated RecipeSteps and RecipeIngredients</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("recentRecipeCards")]

        public async Task<ActionResult<List<Recipe>>> GetRecentRecipeCards( int topX = 12)
        {
            if (topX <= 0 || topX > 12)
            {
                return BadRequest();
            }
            var recipeCards = await _recipeCardService.GetTopXRecentRecipeCards(topX);
            if (recipeCards.Count == 0)
            {
                return NotFound();
            }
            return Ok(recipeCards);
        }


    }

}
