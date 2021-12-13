using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Utils
{
    public static class UrlHelper
    {
        private static string GetUrl()
        {
#if (DEBUG)
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
#else
            var config =  new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
#endif
            return config.GetSection("WebApiUrl").GetSection("v1").Value;
        }

        public static class Api
        {
            public static Uri GateWay = new Uri(GetUrl());
            public static Uri RecipesApi => new Uri($"{GetUrl()}recipes-api/");
      
        }

        public static class Controller
        {
            public const string User = "User/";
            public const string Role = "Role/";
            public const string Recipe = "Recipe/";
        
        }

        /// <summary>
        /// Parameters that are passed to the WebApi
        /// </summary>
        public static class Parameters
        {
            // Parameters
            public const string CustomerId = "CustomerID";
            public const string DbName = "DBName";
            public const string UserId = "UserID";
            public const string waybillId = "waybillId";
        }
    }
}
