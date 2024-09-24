using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagment.Models;

namespace TaskManagment.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromServices] IAuthorizationService authorizationService)
        {

            var t1 =  authorizationService.AuthorizeAsync(User, "AdminOnly");
            var t2 = authorizationService.AuthorizeAsync(User, "Palestine");

            Task.WaitAll(t1, t2);
            if (t1.Result.Succeeded ||t2.Result.Succeeded)
            {

                return View("AdminsOnly");
            }
            else
            {
                return View();

            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
