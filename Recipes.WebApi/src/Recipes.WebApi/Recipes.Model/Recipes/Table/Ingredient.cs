using Dapper.Contrib.Extensions;
using System;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{
    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }
        public string Description { get; set; }
        public int RecipeID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreateByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
