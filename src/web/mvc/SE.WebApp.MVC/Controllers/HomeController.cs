using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;

namespace SE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("service-unavailable")]
        public IActionResult ServiceUnavailable() =>
            View("Error", new ErrorViewModel
            {
                ErrorCode = 500,
                Title = "Service unavailable",
                Message = "The service is temporarily unavailable, this might occour due to user requests overload"
            });

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var model = new ErrorViewModel { ErrorCode = id };

            switch (model.ErrorCode)
            {
                case 500:
                    model.Title = "An error occured";
                    model.Message = "An error occured! Try again later or contact our support line.";
                    break;

                case 404:
                    model.Title = "Page not found";
                    model.Message = "The page you're trying to access doesn't exist! <br/ >If you have any doubts contact our support line.";
                    break;

                case 403:
                    model.Title = "Access denied";
                    model.Message = "You don't have permission do complete this action.";
                    break;

                default:
                    return StatusCode(404);
            }

            return View("Error", model);
        }
    }
}
