using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Data.Entity;

namespace APMT.Areas.System.Controllers
{
    public class UserManagementController : Controller
    {
        CP_SPMEntities1 db = new CP_SPMEntities1();
        // GET: System/ManageUser
        public ActionResult View_List(string searchString)
        {
            if (searchString != null)
            {
                var query = from user in db.APMT_User
                            where (user.Fullname.Contains(searchString) || user.Email.Contains(searchString))
                            select user;

                ViewBag.List = query.OrderByDescending(x => x.ID).ToList();
                int count = query.ToList().Count();
                if (count > 0 && searchString.Length != 0)
                {
                    ViewBag.Result = "Finded " + count + " Result(s)";
                }
                else if (count > 0 && searchString.Length == 0)
                {
                    ViewBag.Result = "";
                }
                else
                {
                    ViewBag.Result = "Haven't result to find";
                    var query1 = from user in db.APMT_User
                                 where (user.Fullname.Contains(searchString) || user.Email.Contains(searchString))
                                 select user;

                    ViewBag.List = query1.OrderByDescending(x => x.ID).ToList();
                }
                return View();
            }
            else
            {
                var lstUser = db.APMT_User.ToList();
                ViewBag.List = lstUser;
                return View();
            }

        }

        public ActionResult setProAdministrator(int? id)
        {
            var user = db.APMT_User.FirstOrDefault(x => x.ID == id);
            user.IsProAdmin = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setUser(int? id)
        {
            var user = db.APMT_User.FirstOrDefault(x => x.ID == id);
            user.IsProAdmin = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setStatus(int? id)
        {
            var user = db.APMT_User.FirstOrDefault(x => x.ID == id);
            if (user.Allowed == 1)
            {
                user.Allowed = 2;
            }
            else
            {
                user.Allowed = 1;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
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