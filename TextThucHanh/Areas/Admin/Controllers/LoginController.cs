using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TextThucHanh.Areas.Admin.Code;
using TextThucHanh.Areas.Admin.Models;


namespace TextThucHanh.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
          
            var result = new Model.AccountModel().Login(model.Username, model.Password);
            if (result && ModelState.IsValid) 
            {
                //Nếu thành công chúng ta cần tạo session
                SessionHelper.SetSession(new UserSession(model.Username));
                return RedirectToAction("getListBook", "Book");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc Password không đúng!");
            }
            return View(model);
        }

    }
}