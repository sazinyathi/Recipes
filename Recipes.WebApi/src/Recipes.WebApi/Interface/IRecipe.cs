using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.WebApi.Interface
{
    public  interface IRecipe
    {
        Task<bool> InsertAsyn(RecipeCustomModel recipeCustomModel);
        Task<bool> UpdateAsync(RecipeCustomModel recipeCustomModel);
        Task<IEnumerable<RecipeCustomModel>> GetAllAsync();
        Task<RecipeCustomModel> GetByIdAsync(int recipeId);
        Task<bool> DeleteAsync(Recipe recipe);
    }
}
