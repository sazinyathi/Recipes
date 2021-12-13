using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.Services;
using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Table;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Roles = "Administrator, Local User")]
        public async Task<IActionResult> Roles()
        {
            var providers = await RoleService.GetAllAsync();
            return View(providers);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            
            var result = await RoleService.InsertAsync(roleViewModel.Role);

            var redirectUrl = $"/Role/Roles";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel roleViewModel)
        {
            var result = await RoleService.UpdateAsync(roleViewModel.Role);

            var redirectUrl = $"/Role/Roles";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }

        public async Task<IActionResult> GetByRoleId(int roleId)
        {

            var model = new RoleViewModel
            {
                Role = await RoleService.GetByIdAsync(roleId)

            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(Role role)
        {

            var result = await RoleService.DeleteAsync(role);

            var redirectUrl = string.Format("/Role/Roles");
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.DeleteSuccess, url = redirectUrl })
                           : RedirectToAction("Message", "Home", new { type = StringHelper.Types.DeleteFailed, url = redirectUrl });


        }
    }
}
