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
    public class ShipperController : Controller
    {
        // GET: Shipper
        
        public ActionResult Index(int page = 1, string searchValue = "")
        {

            var model = new Models.Shipper_Result()
            {
                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Shipper_Count(searchValue),
                Data = CatalogBLL.Shipper_List(page, AppSettings.DefaultPageSize, searchValue)
                
            };
            return View(model);

            //var listOfSuppliers = CatalogBLL.Supplier_List(page, 10, searchValue);
            //int rowCount = CatalogBLL.Supplier_Count(searchValue);
            //ViewBag.row_Count = rowCount;
            //return View(listOfSuppliers);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Shipper";
                Shipper newSuppier = new Shipper();
                newSuppier.ShipperID = 0;
                return View(newSuppier);
            }
            else
            {
                ViewBag.Title = "Edit Shipper";
                Shipper editShipper = CatalogBLL.Shipper_Get(Convert.ToInt32(id));
                if (editShipper == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editShipper);
            }

        }
        [HttpPost]
        public ActionResult Input(Shipper model)
        {
            try
            {   //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", " CompanyName is requied");
                
                if (string.IsNullOrEmpty(model.Phone))
                    ModelState.AddModelError("Phone", " Phone is requied");
                
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Xử lý để đưa dữ liệu vào DB
                if (model.ShipperID == 0)
                {
                    int supplierId = CatalogBLL.Shipper_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Shipper_Update(model);
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
        public ActionResult Delete(string method = "", int[] shipperIDs = null)
        {
            if (shipperIDs != null)
            {
                CatalogBLL.Shipper_Delete(shipperIDs);
            }
            return RedirectToAction("Index");
        }
    }
}