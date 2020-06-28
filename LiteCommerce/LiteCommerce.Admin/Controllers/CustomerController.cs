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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CustomerPaginationResult()
            {
                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Customer_Count(searchValue),
                Data = CatalogBLL.Customer_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Customer";
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = "";
                return View(newCustomer);
            }
            else
            {
                ViewBag.Title = "Edit Customer";
                Customer editCustomer = CatalogBLL.Customer_Get(id);
                if (editCustomer == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editCustomer);
            }

        }
        [HttpPost]
        public ActionResult Input(Customer model)
        {

            try
            {   //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.CustomerID))
                    ModelState.AddModelError("CustomerID", " CustomerID is requied");
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", " CompanyName is requied");
                if (string.IsNullOrEmpty(model.ContactName))
                    ModelState.AddModelError("ContactName", " ContactName is requied");
                if (string.IsNullOrEmpty(model.ContactTitle))
                    ModelState.AddModelError("ContactTitle", " ContactTitle is requied");                
                if (string.IsNullOrEmpty(model.Address))
                    ModelState.AddModelError("Address", " Address is requied");
                if (string.IsNullOrEmpty(model.City))
                    ModelState.AddModelError("City", " City is requied");
                if (string.IsNullOrEmpty(model.Fax))
                    ModelState.AddModelError("Fax", " Fax is requied");
                if (string.IsNullOrEmpty(model.Phone))
                    ModelState.AddModelError("Phone", "Phone í requied");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }                               

                //Xử lý để đưa dữ liệu vào DB
                Customer addCustomer = CatalogBLL.Customer_Get(model.CustomerID);
                if (addCustomer == null)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                {
                    string customerId = CatalogBLL.Customer_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Customer_Update(model);
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
        public ActionResult Delete(string method = "", string[] customerIDs = null)
        {
            if (customerIDs != null)
            {
                CatalogBLL.Customer_Delete(customerIDs);

            }
            return RedirectToAction("Index");
        }
    }
}