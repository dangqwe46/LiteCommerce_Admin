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
    [Authorize(Roles = WebUserRoles.DATA_MANAGER_STAFF)]
    public class EmployeeController : Controller
    {




        /// <summary>
        /// Trang hiển thị: danh sách các supplier, các liên kết đến các chức năng liên quan
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "",string Country="")
        {
            var model = new Models.EmployeePaginatonResult()
            {

                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = HumanResourceBLL.Employee_Count(searchValue,Country),
                Data = HumanResourceBLL.Employee_List(page, AppSettings.DefaultPageSize, searchValue, Country)

            };
            return View(model);
            //để t mở file thg minh ra coi hắn kiểu chi

        }
        /// <summary>
        /// Add or Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new supplier";
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = 0;
                return View(newEmployee);
            }
            else
            {
                //ViewBag.Title = "Edit supplier";
                Employee editEmployee = HumanResourceBLL.Employee_Get(Convert.ToInt32(id));
                if (editEmployee == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editEmployee);
            }

        }
        [HttpPost]
        public ActionResult Input(Employee model)
        {

            if (model.EmployeeID == 0)
            {

                bool isHaveEmail = HumanResourceBLL.Employee_CheckEmail(model.Email, model.EmployeeID);


                if (isHaveEmail == true)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống vui lòng nhập Emai khác");
                    return View(model);
                }
                else
                {
                    int ID = HumanResourceBLL.Employee_Add(model);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                bool isHaveEmail = HumanResourceBLL.Employee_CheckEmail(model.Email, model.EmployeeID);
                if (isHaveEmail == true)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống vui lòng nhập Emai khác");
                    return View(model);
                }
                else
                {
                    Employee dataGet = HumanResourceBLL.Employee_Get(model.EmployeeID);
                    string passTemp = Md5HashCode.GetMd5Hash(model.Password);
                    if (dataGet.Password == model.Password)
                    {
                        model.Password = model.Password;
                    }
                    else
                    {
                        model.Password = passTemp;
                    }
                    bool updateChecked = HumanResourceBLL.Employee_Update(model);
                }

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] employeeIDs = null)
        {
            if (employeeIDs != null)
            {
                HumanResourceBLL.Employee_Delete(employeeIDs);

            }
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Controllers/Content/Images/" + file.FileName));
            return "/Controllers/Content/Images/" + file.FileName;
        }

    }
}