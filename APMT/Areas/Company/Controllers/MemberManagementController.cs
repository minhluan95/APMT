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
            return View();
        }
        public ActionResult getMember()
        {
            var query = from UserC in db.APMT_Company_User
                        join user in db.APMT_User on UserC.User_id equals user.ID
                        join company in db.APMT_Company on UserC.Company_id equals company.ID
                        where company.ID == 1
                        select new userCompany
                        {
                            id = UserC.ID,
                            fullName = user.Fullname,
                            email = user.Email,
                            avartar = user.Avatar,
                            createAt = user.Create_at.ToString(),
                            updateAt = user.Update_at.ToString(),
                            isAllowed = UserC.Allowed,
                            isAdministrator = UserC.isAdministrator,
                            isCreator = UserC.isCreator,
                            isMember = UserC.isMember
                        };
            var lisMember = query.OrderByDescending(x => x.id).ToList();
            return Json(new
            {
                lisMember
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult autocomplete(string term)
        {
            var query = from user in db.APMT_User
                        where (user.Email.Contains(term) || user.Fullname.Contains(term)) && user.Allowed == 1
                        select user.Email + " (" + user.Fullname + ")";
            var filteredItems = query.ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_New(string stringEmail)
        {
            string trimEmail = "";
            string mesg = "";
            if (stringEmail != null)
            {
                string email = stringEmail;
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
                    var user = db.APMT_User.SingleOrDefault(x => x.Email.Equals(trimEmail));
                    if (user != null)
                    {
                        var userID = user.ID;
                        if (user.Allowed == 1)
                        {
                            var existMember = db.APMT_Company_User.SingleOrDefault(x => x.User_id == userID);
                            if (existMember == null)
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
                                mesg = "Successful";

                            }
                            else
                            {
                                mesg = "This user was existed in this company!";
                                //TempData["Message"] = "This user was existed in this company!";
                                //return RedirectToAction("View_List");
                            }
                        }
                        else
                        {
                            mesg = "This user was Blocked";
                            //TempData["Message"] = "This user was Blocked";
                            //return RedirectToAction("View_List");
                        }
                    }
                    else
                    {
                        mesg = "This user not Exist";
                        //TempData["Message"] = "This user not Exist";
                        //return RedirectToAction("View_List");
                    }


                }
                catch (Exception e)
                {
                    mesg = "Add new Failure,Please Try Again !";
                    //TempData["Message"] = "Add new Failure !";
                    //return RedirectToAction("View_List");
                }

            }
            return Json(new
            {
                mesg
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult deleteMember(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            db.APMT_Company_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("View_List");
        }

        public JsonResult setAdministrator(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            user.isAdministrator = !user.isAdministrator;
            db.SaveChanges();
            return Json(new
            {
                user.isAdministrator
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult setCreator(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            user.isCreator = !user.isCreator;
            db.SaveChanges();
            return Json(new
            {
                user.isCreator
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult setMember(int? id)
        {
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            if (user.isMember == false)
            {
                user.isMember = true;
                user.isAdministrator = false;
            }
            else
            {
                user.isMember = false;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }

        public JsonResult setStatus(int? id)
        {
            bool status = true;
            var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
            if (user.Allowed == 1)
            {
                user.Allowed = 0;
                status = false;
            }
            else
            {
                user.Allowed = 1;
                status = true;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult viewInfor_MB(int? id)
        {
            var userCompany = db.APMT_Company_User.SingleOrDefault(x => x.ID == id);

            int? userID = userCompany.User_id;

            APMT_User user = db.APMT_User.Find(userID);

            ViewBag.User = user;

            return View(user);
        }
        public int getId_User(string email)
        {
            APMT_User user = db.APMT_User.SingleOrDefault(x => x.Email.Equals(email));
            if(user!=null)
            {
                return user.ID;
            }
            else
            {
                return 0;
            }
        }
    }
}