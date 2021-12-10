using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using System.Diagnostics;

namespace Recipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string NotApplicable = "N/A";

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (User.FindFirst("UserID") == null)
            {
                return RedirectToAction("Access", "Home");
            }

            return View();
        }

        public IActionResult Roles(string message, string title, string rootPath)
        {
            var messageModel = new MessageModel
            {
                Message = message,
                Title = title,
                RootPath = rootPath

            };
            return View(messageModel);
        }

        public IActionResult Access()
        {
            return View();
        }

        public IActionResult Message(string type, string url, string title, string message)
        {
            if (url == null)
            {
                url = "Home/Index";
            }

            var model = new MessageModel
            {
                Message = Utils.StringHelper.Html.UpdateRecordSuccessMessage,
                //Route = "Index",
                // Controller = "Home",
                Title = Utils.StringHelper.Html.UpdateRecordSuccessTitle,
                Icon = "fas fa-check-circle text-success",
                Url = url,
                RootPath = $"http://{_httpContextAccessor.HttpContext.Request.Host.Value}"
            };

            switch (type)
            {
                case Utils.StringHelper.Types.NoRecords:
                    model.Title = title ?? "Oops";
                    model.Message = message ?? "Something has gone wrong... Contact IT";
                    model.Icon = Utils.StringHelper.Html.FailedIcon;
                    model.Type = "NoRecords";
                    break;
                case Utils.StringHelper.Types.UpdateSuccess:
                    model.Message = Utils.StringHelper.Html.UpdateRecordSuccessMessage;
                    model.Title = Utils.StringHelper.Html.UpdateRecordSuccessTitle;
                    model.Type = null;
                    break;
                case Utils.StringHelper.Types.UpdateFailed:
                    model.Title = string.IsNullOrEmpty(message) ? Utils.StringHelper.Html.UpdateRecordFailedTitle : message;
                    model.Message = Utils.StringHelper.Html.UpdateRecordFailedMessage;
                    model.Icon = Utils.StringHelper.Html.FailedIcon;
                    model.Type = null;
                    break;
                case Utils.StringHelper.Types.SaveSuccess:
                    model.Title = Utils.StringHelper.Html.SaveRecordSuccessMessage;
                    model.Message = Utils.StringHelper.Html.SaveRecordSuccessTitle;
                    model.Icon = Utils.StringHelper.Html.SuccessIcon;
                    model.Type = null;
                    break;
                case Utils.StringHelper.Types.DeleteSuccess:
                    model.Title = Utils.StringHelper.Html.DeleteRecordSuccessMessage;
                    model.Message = Utils.StringHelper.Html.DeleteRecordSuccessTitle;
                    model.Icon = Utils.StringHelper.Html.SuccessIcon;
                    model.Type = null;
                    break;
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode != null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = int.Parse(statusCode.ToString()) });
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
