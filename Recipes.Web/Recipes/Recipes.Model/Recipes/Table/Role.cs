using System;

namespace Recipes.WebApi.Recipes.Model.Table
{

    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? DeletedByUserID { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
