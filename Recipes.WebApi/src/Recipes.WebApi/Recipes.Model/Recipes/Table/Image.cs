using Dapper.Contrib.Extensions;
using System;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public Byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public int RecipeID { get; set; }
    }
}
