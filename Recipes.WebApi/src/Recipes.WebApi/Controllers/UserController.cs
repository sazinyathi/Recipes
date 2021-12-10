using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet("GetAllAsync")]
        [SwaggerOperation(Summary = "Returns List of User", Description = "Returns List of User")]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _user.GetAllAsync();
        }


        [HttpPost("InsertAsync")]
        [SwaggerOperation(Summary = "Post - Inserts the User table", Description = "Returns bool")]
        public async Task<ActionResult<bool>> InsertAsync(User user)
        {
            return await _user.InsertAsyn(user);
        }

        [HttpGet("GetByIdAsync")]
        [SwaggerOperation(Summary = "Returns User", Description = "Returns User")]
        public async Task<User> GetByIdAsync(int userId)
        {
            return await _user.GetByIdAsync(userId);
        }

        [HttpPut("UpdateAsync")]
        [SwaggerOperation(Summary = "Put - Updates the User table", Description = "Returns a bool")]
        public async Task<ActionResult<bool>> UpdateAsync(User user)
        {
            return await _user.UpdateAsync(user);
        }

        [HttpPut("DeleteAsync")]
        [SwaggerOperation(Summary = "Put - Delete the User table", Description = "Returns a bool")]
        public async Task<ActionResult<bool>> DeleteAsync(User user)
        {
            return await _user.DeleteAsync(user);
        }

        [HttpGet("FindByUsernameAsync")]
        [SwaggerOperation(Summary = "Returns User", Description = "Returns User")]
        public async Task<UserInformation> FindByUsernameAsync(string username)
        {
            return await _user.FindByUsernameAsync(username);
        }
    }
}
