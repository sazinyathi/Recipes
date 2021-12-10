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
        public async Task<IActionResult> Roles()
        {
            var providers = await RoleService.GetAllAsync();
            return View(providers);
        }

        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            //var isNameExists = await ProviderService.IsProviderNameExists(providerViewModel.Provider.Name);
            //if (isNameExists)
            //{
            //    var model = new ProviderViewModel
            //    {
            //        Seta = await LookUpCodesService.LookUpCodesByCategoryIDAsync(93),
            //        ErrorMessage = $"{providerViewModel.Provider.Name} exists on database",
            //        Provider = providerViewModel.Provider
            //    };
            //    return View("Create", model);
            //}
            var result = await RoleService.InsertAsync(roleViewModel.Role);

            var redirectUrl = $"/Role/Roles";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }

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
