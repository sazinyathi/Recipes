using System;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{

    public class Image
    {
       public int ImageID { get; set; }
        public Byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public int RecipeID { get; set; }
    }
}
