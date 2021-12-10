using Dapper.Contrib.Extensions;

namespace Recipes.WebApi.Recipes.Model.Recipes.Table
{
    [Table("User")]
    public class User
	{
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
   
	}
}
