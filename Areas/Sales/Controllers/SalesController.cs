using Food_Ordering.Areas.Products.Models;
using Food_Ordering.Auth;
using Food_Ordering.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Food_Ordering.Areas.Sales.Controllers
{
    [Area("Sales")]
    [Route("Sales/[Controller]/[action]")]
    public class SalesController : Controller
    {
        SalesDAL salesDAL = new SalesDAL();
        public IActionResult Index()
        {
            return View();
        }

        [CheckAdminAccess]
        public ActionResult Delete(int SalesId)
        {
            if (Convert.ToBoolean(salesDAL.PR_Sales_Delete(SalesId)))
                return RedirectToAction("Index");
            return View("Index");
        }

        [CheckAdminAccess]
        public IActionResult Add(int? SalesId)
        {
            if (SalesId != null)
            {
                DataTable dt = salesDAL.PR_Sales_Insert(SalesId);
                if (dt.Rows.Count > 0)
                {
                    Areas.Sales.Models.SalesModel salesModel = new Areas.Sales.Models.SalesModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        salesModel.SalesId = (Convert.ToInt32(dr["SalesId"]));
                        salesModel.ProductId = (Convert.ToInt32(dr["ProductId"]));
                        salesModel.Discount = (Convert.ToInt32(dr["Discount"]));
                    }

                    return View("Dshboard", salesModel);
                }
            }
            return View("Dashboard");
        }
    }
}
