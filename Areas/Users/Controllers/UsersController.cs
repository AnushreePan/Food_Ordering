using Food_Ordering.Areas.Users.Models;
using Food_Ordering.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Food_Ordering.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("Users/[Controller]/[action]")]

    public class UsersController : Controller
    {
        UsersDAL usersdal = new UsersDAL();

        private IConfiguration Configuration;
        public UsersController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View("../User/UserLogin");
        }

        public IActionResult Admin()
        {
            return View("../Admin/Login");
        }

        [HttpPost]
        public IActionResult LoginAdmin(Areas.Users.Models.UsersModel usersModel)
        {
            String error = null;

            if (usersModel.Email == null)
            {
                error += "Email is Required";
            }
            if (usersModel.Password == null)
            {
                error += "<br/>Password is Required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Admin");
            }
            else
            {
                DataTable dtusers = usersdal.PR_User_SelectByIDPass(usersModel.Email, usersModel.Password);
                if (dtusers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtusers.Rows)
                    {
                        HttpContext.Session.SetInt32("UserId", Convert.ToInt32(dr["UserId"]));
                        HttpContext.Session.SetString("Name", dr["Name"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("RoleType", dr["RoleType"].ToString());
                        HttpContext.Session.SetString("ImageUrl", dr["ImageUrl"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "Email and Password is Incorect";
                    return RedirectToAction("Admin");
                }
                if (HttpContext.Session.GetString("Email") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Home", "Admin");
                }
            }
            return RedirectToAction("Admin");
        }

        public IActionResult LoginUser(Areas.Users.Models.UsersModel usersModel)
        {
            String error = null;

            if (usersModel.Email == null)
            {
                error += "Email is Required";
            }
            if (usersModel.Password == null)
            {
                error += "<br/>Password is Required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                DataTable dtusers = usersdal.PR_User_SelectByIDPass(usersModel.Email, usersModel.Password);
                if (dtusers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtusers.Rows)
                    {
                        HttpContext.Session.SetInt32("UserId", Convert.ToInt32(dr["UserId"]));
                        HttpContext.Session.SetString("Name", dr["Name"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("RoleType", dr["RoleType"].ToString());
                        HttpContext.Session.SetString("ImageUrl", dr["ImageUrl"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "Email and Password is Incorect";
                    return RedirectToAction("Index");
                }
                if (HttpContext.Session.GetString("Email") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
