using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class RoleService
    {
        private const string url = @"http://localhost:62733/api/";
        public static async Task<List<Role>> GetAllAsync()
        {
            return await RestApiHelper.GetAsync<List<Role>>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Role}GetAllAsync"));
        }

        //public static async Task<bool> IsProviderNameExists(string providerName)
        //{
        //    return await RestApiHelper.GetAsync<bool>(new Uri(UrlHelper.Api.RecruitmentApi, $"{UrlHelper.Controller.Provider}IsProviderNameExists?providerName={providerName}"));
        //}

        public static async Task<Role> GetByIdAsync(int roleId)
        {
            return await RestApiHelper.GetAsync<Role>(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Role}GetByIdAsync?roleId={roleId}"));
        }

        public static async Task<bool> UpdateAsync(Role role)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Role}UpdateAsync"), role);
        }

        public static async Task<bool> InsertAsync(Role role)
        {
            return await RestApiHelper.InsertAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Role}InsertAsync"), role);
        }
        public static async Task<bool> DeleteAsync(Role role)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecipesApi, $"{url}{UrlHelper.Controller.Role}DeleteAsync"), role);
        }

    }
}
