using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
    public class SubMenu
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Role { get; set; }
        public string SubTitle { get; set; }
        public string Svg { get; set; }
    }

    public class MainMenu
    {
        [Required]
        public string MainTitle { get; set; }

        [Required]
        public string SubTitle { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }

    public class ParentMenu
    {
        [Required]
        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
