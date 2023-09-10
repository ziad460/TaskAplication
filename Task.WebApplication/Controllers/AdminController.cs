using Microsoft.AspNetCore.Mvc;

namespace Task.WebApplication.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
