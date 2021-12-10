using Dapper.Contrib.Extensions;

namespace Recipes.WebApi.Recipes.Model.Table
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
       
    }
}
