using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheBerbakery.API.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("RecipeCard.API/[controller]")]
    [ApiController]
    public class RecipeCardController : ControllerBase
    {
        #region Service Constructor

        #endregion

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("recipeCards")]

    }

}
