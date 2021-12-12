using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserID { get; set; }

        public int ? DeletedBy { get; set; }
        public DateTime ? DeletedOn { get; set; }
    }
}
