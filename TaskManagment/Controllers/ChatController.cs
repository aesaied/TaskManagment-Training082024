using Microsoft.AspNetCore.Mvc;

namespace TaskManagment.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
