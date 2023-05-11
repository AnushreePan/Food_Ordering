using Microsoft.AspNetCore.Mvc;
using Food_Ordering.Auth;
namespace Food_Ordering.Areas
{
    public class AdminController : Controller
    {
        [CheckAdminAccess]
        public IActionResult Home()
        {
            return View("../Admin/Dashboard");
        }
        public IActionResult Index()
        {
            return View("../Admin/Login");
        }
        public IActionResult Register()
        {
            return View("../Admin/Register");
        }
    }
}
