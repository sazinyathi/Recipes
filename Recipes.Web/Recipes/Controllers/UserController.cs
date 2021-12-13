using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.Recipes.Services;
using Recipes.Services;
using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    [Authorize(Roles = "Administrator, Local User")]
    public class UserController : Controller
    {
        public async Task<IActionResult> Users()
        {
            var users = await UserService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            var model = new UserViewModel
            {
                Roles = await RoleService.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
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
            var result = await UserService.InsertAsync(userViewModel.User);

            var redirectUrl = $"/User/Users";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }


        public async Task<IActionResult> GetByUserId(int userId)
        {

            var model = new UserViewModel
            {
                Roles = await RoleService.GetAllAsync(),
                User = await UserService.GetByIdAsync(userId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(User user)
        {

            var result = await UserService.DeleteAsync(user);

            var redirectUrl = string.Format("/User/Users");
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.DeleteSuccess, url = redirectUrl })
                           : RedirectToAction("Message", "Home", new { type = StringHelper.Types.DeleteFailed, url = redirectUrl });


        }

        [HttpPost]

        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            var result = await UserService.UpdateAsync(userViewModel.User);

            var redirectUrl = $"/User/Users";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }

        
    }
}
