using LIbrary.Services.EmailSender;
using Microsoft.AspNetCore.Mvc;

namespace LIbrary.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
