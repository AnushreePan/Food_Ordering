using Food_Ordering.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Food_Ordering.Auth;

namespace Food_Ordering.Areas.Category.Controllers
{
    [CheckAdminAccess]
    [Area("Category")]
    [Route("Category/[Controller]/[action]")]
    public class CategoryController : Controller
    {
        CategoryDAL categoryDAL = new CategoryDAL();
        public IActionResult Index()
        {
            DataTable dtcategory = categoryDAL.PR_Category_SelectAll();
            return View("CategoryList", dtcategory);

        }
        public ActionResult Delete(int CategoryId)
        {
            if (Convert.ToBoolean(categoryDAL.PR_Category_Delete(CategoryId)))
                return RedirectToAction("Index");
            return View("Index");
        }
        public IActionResult Add(int? CategoryId)
        { 
            if (CategoryId != null)
            {
                DataTable dt = categoryDAL.PR_Category_SelectByPK(CategoryId);
                if (dt.Rows.Count > 0)
                {
                    Areas.Category.Models.CategoryModel categoryModel = new Areas.Category.Models.CategoryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        categoryModel.CategoryId = (Convert.ToInt32(dr["CategoryId"]));
                        categoryModel.CategoryName = (Convert.ToString(dr["CategoryName"]));
                        categoryModel.ImageUrl = (Convert.ToString(dr["ImageUrl"]));
                        categoryModel.CreatedDate = (Convert.ToDateTime(dr["CreatedDate"]));
                    }

                    return View("CategoryForm", categoryModel);
                }
            }
            return View("CategoryForm");
        }
        public IActionResult Save(Areas.Category.Models.CategoryModel categoryModel)
        {
            if (categoryModel.File != null)
            {
                string FilePath = "wwwroot\\CategoryImages";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, categoryModel.File.FileName);
                categoryModel.ImageUrl = FilePath.Replace("wwwroot\\", "/") + "/" + categoryModel.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    categoryModel.File.CopyTo(stream);
                }

            }

            if (Convert.ToBoolean(categoryDAL.PR_Category_Save(categoryModel.CategoryId, categoryModel.CategoryName, categoryModel.ImageUrl)))
            {
                if (categoryModel.CategoryId == null)
                {
                    TempData["CategoryInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CategoryInsetMsg"] = "Record Updated Successfully";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
