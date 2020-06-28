using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = WebUserRoles.ADMINISTRATOR)]
    public class SupplierController : Controller
    {
        /// <summary>
        /// Trang hiển thị các chức năng các suppliers, các liên kết đến các chức năng liên quan
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page=1, string searchValue="")
        {

            var model = new Models.SupplierPaginationResult()
            {
                SearchValue =searchValue,
                Page = page,
                PageSize=AppSettings.DefaultPageSize,
                RowCount= CatalogBLL.Supplier_Count(searchValue),
                Data= CatalogBLL.Supplier_List(page, AppSettings.DefaultPageSize, searchValue)

            };

            //var listOfSuppliers = CatalogBLL.Supplier_List(page, 10, searchValue);
            //int rowSuppliers = CatalogBLL.Supplier_Count(searchValue);
            //ViewBag.RowCount = rowSuppliers;
            return View(model);//chuyển dữ liệu thông qua Model
        }
        /// <summary>
        /// Add or Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Input(string id="")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Supplier";
                Supplier newSupplier = new Supplier();
                newSupplier.SupplierID = 0;
                return View(newSupplier);
            }
            else
            {
                ViewBag.Title = "Edit Supplier";
                Supplier editSupplier = CatalogBLL.Supplier_Get(Convert.ToInt32(id));
                if (editSupplier == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editSupplier);

            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Supplier model)         
        {

            try
            {   //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", " CompanyName is requied");
                if (string.IsNullOrEmpty(model.ContactName))
                    ModelState.AddModelError("ContactName", " ContactName is requied");
                if (string.IsNullOrEmpty(model.ContactTitle))
                    ModelState.AddModelError("ContactTitle", " ContactTitle is requied");
                if (string.IsNullOrEmpty(model.Phone))    
                    ModelState.AddModelError("Phone", " Phone is requied");
                if (string.IsNullOrEmpty(model.Address))
                    ModelState.AddModelError("Address", " Address is requied");
                if (string.IsNullOrEmpty(model.City))
                    ModelState.AddModelError("City", " City is requied");
                if (string.IsNullOrEmpty(model.Fax))
                    ModelState.AddModelError("Fax", " Fax is requied");
                if (string.IsNullOrEmpty(model.HomePage))
                    ModelState.AddModelError("HomePage", " HomePage is requied");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Xử lý để đưa dữ liệu vào DB
                if (model.SupplierID == 0)
                {
                    int supplierId = CatalogBLL.Supplier_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Supplier_Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", ex.Message+":"+ex.StackTrace);
                return View(model);
                    
            }
            
        }
        [HttpPost]
        public ActionResult Delete(string method="", int[] supplierIDs=null)
        {
            if (supplierIDs != null)
            {
                CatalogBLL.Supplier_Delete(supplierIDs);

            }
            return RedirectToAction("Index");
        }
        
                

    }
}