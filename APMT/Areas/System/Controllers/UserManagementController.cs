using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APMT.Areas.System.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: System/ManageUser
        public ActionResult View_List()
        {
            return View();
        }
        public ActionResult View_Details()
        {
            return View();
        }
        public ActionResult Update_Profile()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(FormCollection f)
        //{
        //    string Email = f["txtEmail"].ToString();
        //    string Password = f["txtPassword"].ToString();
        //    APMT_Users user = db.APMT_Users.SingleOrDefault(x => x.Email == Email && x.Passwords == Password);
        //    if (user != null)
        //    {
        //        Session["User"] = user;

        //        return RedirectToAction("Index", "ManageMember");
        //    }
        //    return RedirectToAction("Index", "ManageMember");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model)
        //{
        //    var result = new UserModel().Login(model.Email, model.Password);
        //    if (result && ModelState.IsValid)
        //    {
        //        SessionHelper.SetSession(new UserSession() { @Email = model.Email });
        //        return RedirectToAction("Index", "ManageCompany");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "email or password incorrect");
        //    }
        //    return View(model);
        //}
    }
}