using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipe _recipe;
        public RecipeController(IRecipe recipe)
        {
            _recipe = recipe;
        }

        [HttpPost("InsertAsync")]
        [SwaggerOperation(Summary = "Post - Inserts the Recipe table", Description = "Returns bool")]
        public async Task<ActionResult<bool>> InsertAsync(RecipeCustomModel recipeCustomModel)
        {
            return await _recipe.InsertAsyn(recipeCustomModel);
        }
    }
}
