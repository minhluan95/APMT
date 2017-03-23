using APMT.Areas.Company.Models;
using Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace APMT.Areas.Company.Controllers
{
    public class MemberManagementController : Controller
    {
        private CP_SPMEntities1 db = new CP_SPMEntities1();

        // GET: Company/ManageMember
        public ActionResult View_List()
        {
            var query = from UserC in db.APMT_Company_User
                        join user in db.APMT_User on UserC.User_id equals user.ID
                        join company in db.APMT_Company on UserC.Company_id equals company.ID
                        where company.ID == 1
                        select new userCompany { id = UserC.ID,
                            fullName = user.Fullname,
                            email = user.Email,
                            avartar = user.Avatar,
                            createAt = user.Create_at.ToString(),
                            updateAt = user.Update_at.ToString(),
                            isAllowed = UserC.Allowed,
                            isAdministrator = UserC.isAdministrator,isCreator=UserC.isCreator,isMember=UserC.isMember};
      
            ViewBag.List = query.OrderByDescending(x => x.id).ToList();
            return View();
        }

        public ActionResult autocomplete(string term)
        {
            var query = from user in db.APMT_User
                        where (user.Email.Contains(term) || user.Fullname.Contains(term)) && user.Allowed == 1
                        select user.Email + " (" + user.Fullname + ")";
            var filteredItems = query.ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_New(FormCollection f)
        {
            ViewBag.Message = null;
            string trimEmail = "";
            string email = f["somevalue"];
           // string role = f["selectRole"];
            if (email.Contains("("))
            {
                trimEmail = email.Substring(0, email.IndexOf('(')).Trim();
            }
            else
            {
                trimEmail = email;
            }
            try
            {
                var userID = db.APMT_User.SingleOrDefault(x => x.Email.Equals(trimEmail)).ID;
                if (userID != null)
                {                  
                    APMT_Company_User companyUser = new APMT_Company_User();
                    companyUser.Company_id = 1;
                    companyUser.User_id = int.Parse(userID.ToString());
                    companyUser.Allowed = 1;
                    companyUser.isMember = true;
                    companyUser.isAdministrator = false;
                    companyUser.isCreator = false;
                    db.APMT_Company_User.Add(companyUser);
                    db.SaveChanges();
                    TempData["Message"] = "Successful";
                        return RedirectToAction("View_List");                  
                }
                else
                {
                    TempData["Message"] = "User is not exist !";
                    return RedirectToAction("View_List");
                }
            }
            catch (Exception e)
            {
                TempData["Message"] = "Add new Failure !";
                return RedirectToAction("View_List");
            }
        }

        public ActionResult deleteMember(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            db.APMT_Company_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setAdministrator(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            if(user.isAdministrator == false)
            { 
            user.isAdministrator = true;
            }
            else
            {
                user.isAdministrator = false;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setCreator(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            if (user.isCreator == false)
            {
                user.isCreator = true;
            }
            else
            {
                user.isCreator = false;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setMember(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            if (user.isMember == false)
            {
                user.isMember = true;
            }
            else
            {
                user.isMember = false;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public ActionResult setStatus(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
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

        //[HttpPost]
        //public ActionResult deleteMember(APMT_Company_User user)
        //{
        //    db.APMT_Company_User.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("View_List");
        //}
    }
}