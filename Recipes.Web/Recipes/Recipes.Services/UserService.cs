using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class UserService
    {
        private const string url = @"http://localhost:62733/api/";
        public static async Task<UserInformation> FindByUsernameAsync(string userName)
        {
            return await RestApiHelper.GetAsync<UserInformation>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}FindByUsernameAsync?username={userName}"));
        }

        public static async Task<List<User>> GetAllAsync()
        {
            return await RestApiHelper.GetAsync<List<User>>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}GetAllAsync"));
        }

        public static async Task<User> GetByIdAsync(int roleId)
        {
            return await RestApiHelper.GetAsync<User>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}GetByIdAsync?userId={roleId}"));
        }

        public static async Task<bool> UpdateAsync(User user)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}UpdateAsync"), user);
        }

        public static async Task<bool> InsertAsync(User user)
        {
            return await RestApiHelper.InsertAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}InsertAsync"), user);
        }
        public static async Task<bool> DeleteAsync(User user)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.User}DeleteAsync"), user);
        }

    }
}
