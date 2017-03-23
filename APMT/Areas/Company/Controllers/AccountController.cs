using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace APMT.Areas.Company.Controllers
{
    public class AccountController : BaseController
    {
        private CP_SPMEntities1 db = new CP_SPMEntities1();
        [Route("/company/log-in")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Login(string email, string password)
        {
            var user = db.APMT_User.FirstOrDefault(x => x.Email == email && x.Password == password && x.Allowed == 1);
            if (user != null)
            {

                Session["userid"] = user.ID;
                Session["email"] = user.Email;
                Session["fullname"] = user.Fullname;
                Session["avatar"] = user.Avatar;
                Session["createat"] = user.Create_at;

                return RedirectToAction("Index","Dashboard");

            }
            ViewBag.error = "Login fail!";
            return View();
        }
        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["email"] = null;
            Session["fullname"] = null;
            Session["avatar"] = null;
            Session["userpermission"] = null;
            return RedirectToAction("Login");
        }
        [Route("page-not-found")]
        public ActionResult PageNotFound()
        {
            return View();
        }
        
    }
}