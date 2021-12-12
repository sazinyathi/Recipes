using Dapper.Contrib.Extensions;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{
    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }
        public string Description { get; set; }
        public int RecipeID { get; set; }
    }
}
