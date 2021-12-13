using Recipes.WebApi.Recipes.Model.Recipes.Table;

namespace Recipes.WebApi.Recipes.Model.Recipes.Custom
{
    public class RecipeCustomModel
    {
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public Image Image { get; set; }
        public string ErrorMessage { get; set; }
    }
}
