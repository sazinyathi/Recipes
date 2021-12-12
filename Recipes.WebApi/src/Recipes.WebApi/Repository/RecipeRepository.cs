using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using Recipes.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Repository
{
    public class RecipeRepository : IRecipe
    {
        private readonly IConfiguration _configuration;
        public RecipeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Recipe recipe)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                _ = await connection.UpdateAsync(recipe);
                // Return success
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public Task<IEnumerable<RecipeCustomModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeCustomModel> GetByIdAsync(int recipeId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyn(RecipeCustomModel recipeCustomModel)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                using (var transaction = connection.BeginTransaction())
                {
                    _ = await connection.InsertAsync(recipeCustomModel.Recipe);
                    const string sql = "SELECT last_insert_rowid()";

                    var recipeID = connection.QueryFirstOrDefault<int>(sql);
                    recipeCustomModel.Ingredient.RecipeID = recipeID;
                    recipeCustomModel.Image.RecipeID = recipeID;
                    _ = await connection.InsertAsync(recipeCustomModel.Ingredient);
                    _ = await connection.InsertAsync(recipeCustomModel.Image);

                    transaction.Commit();
                    // Return success
                    return true;
                }
            }
            catch (Exception e)
            {
                // Log error
                return false;
            }
        }

        public Task<bool> UpdateAsync(RecipeCustomModel recipeCustomModel)
        {
            throw new NotImplementedException();
        }
    }
}
