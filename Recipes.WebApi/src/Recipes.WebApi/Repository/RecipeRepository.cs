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

        public async Task<IEnumerable<RecipeCustomModel>> GetAllAsync()
        {
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            var query = @"
                           SELECT R.RecipeID AS [RecipeID], R.*,I.ImageID AS [ImageID], I.*,  ID.IngredientID AS [IngredientID], ID.* 
	                            FROM Recipe R
	                       INNER JOIN [Image] I ON I.RecipeID = R.RecipeID
	                       INNER JOIN Ingredient ID ON ID.IngredientID = R.RecipeID
	                       WHERE R.DeletedOn = NULL AND R.DeletedByUserID = NULL
	                       ORDER BY R.RecipeID DESC

                        ";

            var recipes = new List<RecipeCustomModel>();

            var data = connection.Query<Recipe, Image, Ingredient, List<RecipeCustomModel>>(
                 query, (recipe, image, ingredient) =>
                 {
                     var model = new RecipeCustomModel
                     {
                         Recipe = recipe,
                         Image = image,
                         Ingredient = ingredient
                     };

                     recipes.Add(model);
                     return recipes;
                 },

                 splitOn: "RecipeID, ImageID,IngredientID ", commandTimeout: 120).ToList();

            return recipes;
        }

        public async Task<RecipeCustomModel> GetByIdAsync(int recipeId)
        {
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            var query = @"
                           SELECT R.RecipeID AS [RecipeID], R.*,I.ImageID AS [ImageID], I.*,  ID.IngredientID AS [IngredientID], ID.* 
	                            FROM Recipe R
	                       INNER JOIN [Image] I ON I.RecipeID = R.RecipeID
	                       INNER JOIN Ingredient ID ON ID.IngredientID = R.RecipeID
	                       WHERE R.DeletedOn = NULL AND R.DeletedByUserID = NULL
	                       ORDER BY R.RecipeID DESC

                        ";
            
           var data = connection.Query<Recipe, Image, Ingredient, RecipeCustomModel>(
                query, (recipe, image, ingredient) =>
                {
                    return new RecipeCustomModel
                    {
                        Recipe = recipe,
                        Image = image,
                        Ingredient = ingredient
                    };
                },

        splitOn: "RecipeID, ImageID,IngredientID ", commandTimeout: 120).FirstOrDefault();

            return data == null ? new RecipeCustomModel() : data;

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

        public async Task<bool> UpdateAsync(RecipeCustomModel recipeCustomModel)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                using (var transaction = connection.BeginTransaction())
                {
                    _ = await connection.UpdateAsync(recipeCustomModel.Recipe);

                    _ = await connection.UpdateAsync(recipeCustomModel.Ingredient);
                    _ = await connection.UpdateAsync(recipeCustomModel.Image);

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
    }
}
