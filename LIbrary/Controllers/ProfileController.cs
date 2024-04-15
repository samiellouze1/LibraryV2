using Microsoft.AspNetCore.Mvc;

namespace LIbrary.Controllers
{
    public class ProfileController:Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "My Profile";
            return View();
        }
    }
}
