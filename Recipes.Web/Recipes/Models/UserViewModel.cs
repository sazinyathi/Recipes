using Recipes.WebApi.Recipes.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public string ErrorMessage { get; set; }
    }
}
