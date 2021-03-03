using Microsoft.AspNetCore.Mvc;

namespace MeControla.Chat.Server.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
            => View();

        public IActionResult Error()
            => View();
    }
}