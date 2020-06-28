using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.ADMINISTRATOR)] //kiem tra login
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {

            var model = new Models.Category_Result()
            {
                SearchValue = searchValue,
                Page = page,
                RowCount = CatalogBLL.Category_Count(searchValue),
                Data = CatalogBLL.Category_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

            //var listOfSuppliers = CatalogBLL.Supplier_List(page, 10, searchValue);
            //int rowCount = CatalogBLL.Supplier_Count(searchValue);
            //ViewBag.row_Count = rowCount;
            //return View(listOfSuppliers);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Category";
                Category newCategory = new Category();
                newCategory.CategoryID = 0;
                return View(newCategory);
            }
            else
            {
                ViewBag.Title = "Edit Category";
                Category editCategory = CatalogBLL.Category_Get(Convert.ToInt32(id));
                if (editCategory == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editCategory);
            }

        }
        [HttpPost]
        public ActionResult Input(Category model)
        {
            try
            {   //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.CategoryName))
                    ModelState.AddModelError("CategoryName", " CategoryName is requied");

                if (string.IsNullOrEmpty(model.Description))
                    ModelState.AddModelError("Description", " Description is requied");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Xử lý để đưa dữ liệu vào DB
                if (model.CategoryID == 0)
                {
                    int categoryId = CatalogBLL.Category_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Category_Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);

            }
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] categoryIDs = null)
        {
            if (categoryIDs != null)
            {
                CatalogBLL.Category_Delete(categoryIDs);
            }
            return RedirectToAction("Index");
        }

    }
}