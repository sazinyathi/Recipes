using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Recipes.Services;
using Recipes.Utils;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    [Authorize(Roles = "Administrator, Local User")]
    
    public class RecipeController : Controller
    {
        public async Task<IActionResult> Recipes()
        {
            var recipes = await RecipeService.GetAllAsync();
            return View(recipes);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View(new RecipeCustomModel());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(RecipeCustomModel recipeCustomModel, IFormFile files)
        {

            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var image = new Image()
                    {
                       ImageID  = 0,
                       ImageName = newFileName,
                       ImageExtension = fileName

                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        image.ImageData = target.ToArray();
                    }

                    recipeCustomModel.Image = image;
                }

                var result = await RecipeService.InsertAsync(recipeCustomModel);

                var redirectUrl = $"/Recipe/Recipes";
                return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                              : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });


            }
            else
            {
                var result = false;
                var redirectUrl = $"/Recipe/Recipes";
                return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                              : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });
            }

        }


        public async Task<IActionResult> GetByRecipeId(int recipeId)
        {

            var model = await RecipeService.GetByIdAsync(recipeId);
            
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Update(RecipeCustomModel recipeCustomModel)
        {
            var result = await RecipeService.UpdateAsync(recipeCustomModel);

            var redirectUrl = $"/Recipe/Recipes";
            return result ? RedirectToAction("Message", "Home", new { type = StringHelper.Types.SaveSuccess, url = redirectUrl })
                          : RedirectToAction("Message", "Home", new { type = StringHelper.Types.UpdateFailed, url = redirectUrl });

        }
    }
}
