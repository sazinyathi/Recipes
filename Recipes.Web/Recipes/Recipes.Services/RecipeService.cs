using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.Recipes.Services
{
    public class RecipeService
    {
        private const string url = @"http://localhost:62733/api/";
       
        public static async Task<List<RecipeCustomModel>> GetAllAsync()
        {
            return await RestApiHelper.GetAsync<List<RecipeCustomModel>>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Recipe}GetAllAsync"));
        }

        public static async Task<RecipeCustomModel> GetByIdAsync(int recipeId)
        {
            return await RestApiHelper.GetAsync<RecipeCustomModel>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Recipe}GetByIdAsync?recipeId={recipeId}"));
        }

        public static async Task<bool> UpdateAsync(RecipeCustomModel recipeCustomModel)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Recipe}UpdateAsync"), recipeCustomModel);
        }

        public static async Task<bool> InsertAsync(RecipeCustomModel recipeCustomModel)
        {
            return await RestApiHelper.InsertAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Recipe}InsertAsync"), recipeCustomModel);
        }
        public static async Task<bool> DeleteAsync(Recipe recipe)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Recipe}DeleteAsync"), recipe);
        }
    }
}
