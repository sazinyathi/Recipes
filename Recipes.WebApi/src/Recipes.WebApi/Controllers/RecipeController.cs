using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
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

        [HttpGet("GetAllAsync")]
        [SwaggerOperation(Summary = "Returns List of Recipe", Description = "Returns List of Recipe")]
        public async Task<IEnumerable<RecipeCustomModel>> GetAllAsync()
        {
            return await _recipe.GetAllAsync();
        }

        [HttpGet("GetByIdAsync")]
        [SwaggerOperation(Summary = "Returns Recipe", Description = "Returns Recipe")]
        public async Task<RecipeCustomModel> GetByIdAsync(int recipeId)
        {
            return await _recipe.GetByIdAsync(recipeId);
        }

        [HttpPut("UpdateAsync")]
        [SwaggerOperation(Summary = "Put - Updates the Recipe table", Description = "Returns a bool")]
        public async Task<bool> UpdateAsync(RecipeCustomModel recipeCustomModel)
        {
            return await _recipe.UpdateAsync(recipeCustomModel);
        }

        [HttpPut("DeleteAsync")]
        [SwaggerOperation(Summary = "Put - Delete the Recipe table", Description = "Returns a bool")]
        public async Task<bool> DeleteAsync(Recipe recipe)
        {
            return await _recipe.DeleteAsync(recipe);
        }
    }
}
