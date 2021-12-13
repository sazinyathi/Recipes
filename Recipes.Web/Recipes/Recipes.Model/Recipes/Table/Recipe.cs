using System;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{

    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreateByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
