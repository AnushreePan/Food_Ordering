using Food_Ordering.Areas.Products.Models;
using Food_Ordering.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Food_Ordering.Auth;

namespace Food_Ordering.Areas.Products.Controllers
{
    [Area("Products")]
    [Route("Products/[Controller]/[action]")]
    public class ProductsController : Controller
    {
        ProductsDAL productsDAL = new ProductsDAL();
        CategoryDAL categoryDAL = new CategoryDAL();

        [CheckAdminAccess]
        public IActionResult Index()
        {
            DataTable dtproducts = productsDAL.PR_Products_SelectAll();
            return View("ProductsList", dtproducts);

        }
        public IActionResult User()
        {
            DataTable dtproducts = productsDAL.PR_Products_SelectAll();
            return View("UsersProductsList", dtproducts);

        }

        [CheckAdminAccess]
        public ActionResult Delete(int ProductId)
        {
            if (Convert.ToBoolean(productsDAL.PR_Products_Delete(ProductId)))
                return RedirectToAction("Index");
            return View("Index");
        }

        [CheckAdminAccess]
        public IActionResult Add(int? ProductId)
        {
            DataTable dtDropdownCategory = categoryDAL.PR_Category_Dropdown();

            List<Areas.Category.Models.CategoryDropDownModel> listCategory = new List<Areas.Category.Models.CategoryDropDownModel>();
            foreach (DataRow dr in dtDropdownCategory.Rows)
            {
                Areas.Category.Models.CategoryDropDownModel category = new Areas.Category.Models.CategoryDropDownModel();
                category.CategoryId = (Convert.ToInt32(dr["CategoryId"]));
                category.CategoryName = (Convert.ToString(dr["CategoryName"]));
                listCategory.Add(category);
            }
            ViewBag.CategoryList = listCategory;
            
            if (ProductId != null)
            {
                DataTable dt = productsDAL.PR_Products_SelectByPK(ProductId);
                if (dt.Rows.Count > 0)
                {
                    Areas.Products.Models.ProductsModel productsModel = new Areas.Products.Models.ProductsModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        productsModel.ProductId = (Convert.ToInt32(dr["ProductId"]));
                        productsModel.ProductName = (Convert.ToString(dr["ProductName"]));
                        productsModel.Description = (Convert.ToString(dr["Description"]));
                        productsModel.Price = (Convert.ToDecimal(dr["Price"]));
                        productsModel.Quantity = (Convert.ToInt32(dr["Quantity"]));
                        productsModel.ImageUrl = (Convert.ToString(dr["ImageUrl"]));
                        productsModel.CategoryId = (Convert.ToInt32(dr["CategoryId"]));
                        productsModel.CreatedDate = (Convert.ToDateTime(dr["CreatedDate"]));
                    }

                    return View("ProductsForm", productsModel);
                }
            }
            return View("ProductsForm");
        }

        [CheckAdminAccess]
        public IActionResult Save (Areas.Products.Models.ProductsModel productsModel)
        {
            if (productsModel.File != null)
            {
                string FilePath = "wwwroot\\ProductsImages";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, productsModel.File.FileName);
                productsModel.ImageUrl = FilePath.Replace("wwwroot\\", "/") + "/" + productsModel.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    productsModel.File.CopyTo(stream);
                }

            }

            if (Convert.ToBoolean(productsDAL.PR_Products_Save(productsModel.ProductId, productsModel.ProductName, productsModel.Description, productsModel.Price, productsModel.Quantity, productsModel.ImageUrl, productsModel.CategoryId)))
            {
                if (productsModel.ProductId == null)
                {
                    TempData["ProductInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ProductInsetMsg"] = "Record Updated Successfully";
                }
            }

            return RedirectToAction("Index");
        }

    }
}
