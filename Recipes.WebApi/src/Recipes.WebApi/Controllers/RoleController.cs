using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Table;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpGet("GetAllAsync")]
        [SwaggerOperation(Summary = "Returns List of Role", Description = "Returns List of Role")]
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _role.GetAllAsync();
        }


        [HttpPost("InsertAsync")]
        [SwaggerOperation(Summary = "Post - Inserts the Role table", Description = "Returns bool")]
        public async Task<ActionResult<bool>> InsertAsync(Role role)
        {
            return await _role.InsertAsyn(role);
        }

        [HttpGet("GetByIdAsync")]
        [SwaggerOperation(Summary = "Returns Role", Description = "Returns Course")]
        public async Task<Role> GetByIdAsync(int roleId)
        {
            return await _role.GetByIdAsync(roleId);
        }

        [HttpPut("UpdateAsync")]
        [SwaggerOperation(Summary = "Put - Updates the Role table", Description = "Returns a bool")]
        public async Task<ActionResult<bool>> UpdateAsync(Role role)
        {
            return await _role.UpdateAsync(role);
        }

        [HttpPut("DeleteAsync")]
        [SwaggerOperation(Summary = "Put - Delete the Role table", Description = "Returns a bool")]
        public async Task<ActionResult<bool>> DeleteAsync(Role role)
        {
            return await _role.DeleteAsync(role);
        }

    }
}
