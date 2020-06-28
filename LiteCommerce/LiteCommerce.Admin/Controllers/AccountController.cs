using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// Giao diện quản lý Account
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Account
        public ActionResult Index()
        {
            WebUserData userData = User.GetUserData();
            Employee data = HumanResourceBLL.Employee_GetByEmail(userData.UserID);
            return View(data);
        }
        [HttpPost]
        public ActionResult ChangeInfo(Employee data)
        {

            if (data != null)
            {

                Employee getEmployee = HumanResourceBLL.Employee_Get(data.EmployeeID);

                data.Password = getEmployee.Password;
                data.Notes = getEmployee.Notes;
                data.GroupNames = getEmployee.GroupNames;
                data.Email = getEmployee.Email;
                data.PhotoPath = getEmployee.PhotoPath;
                bool editUser = HumanResourceBLL.Employee_Update(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePwd()
        {
            return View();
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account"); //quay lai, khong view
        }
        /// <summary>
        /// Xử lý login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(String email = "", String password = "")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                var userAccount = UserAccountBLL.Authorize(email, password, UserAccountTypes.Employee);
                if (userAccount != null)
                {
                    WebUserData cookieData = new Admin.WebUserData()
                    {
                        UserID = userAccount.UserID,
                        FullName = userAccount.FullName,
                        Photo = userAccount.Photo,
                        GroupName = userAccount.GroupName,
                        LoginTime = DateTime.Now,
                        SessionID = Session.SessionID,
                        ClientIP = Request.UserHostAddress
                    };
                    FormsAuthentication.SetAuthCookie(cookieData.ToCookieString(), false);
                    return RedirectToAction("Index", "Dashboard");


                }
                else
                {

                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                    ViewBag.Email = email;
                    return View();
                }



                //bool checkLogin = EmployeeBLL.Employee_CheckLogin(email, password);
                //if (checkLogin)
                //{
                //    System.Web.Security.FormsAuthentication.SetAuthCookie(email, false);
                //    return RedirectToAction("Index", "Dashboard");
                //}
                //else
                //{

                //    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                //    ViewBag.Email = email;
                //    return View();
                //}
            }
        }
        [AllowAnonymous]
        /// <summary>
        /// Lấy lại mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPwd()
        {
            //TODO: Xây dựng
            return View();
        }

       
        /// <summary>
        /// upload anh
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Images/" + file.FileName));
            return "/Images/" + file.FileName;
        }
    }
}