using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Models;
using APMT.Areas.Company.Models;
using System.Globalization;

namespace APMT.Areas.Company.Controllers
{
    public class ProjectManagementController : Controller
    {
        CP_SPMEntities1 db = new CP_SPMEntities1();
        public ActionResult View_List()
        {
            var query = from project in db.APMT_Project
                        join user in db.APMT_User on project.Manager_Id equals user.ID
                        join company in db.APMT_Company on project.Company_Id equals company.ID
                        where company.ID == 1 //mố login truyền id zo
                        select new managerProject
                        {
                            ID = project.ID,
                            ProjectName = project.Name,
                            ManagerName = user.Fullname,
                            StartDay = project.StartDay.ToString(),
                            EndDay = project.EndDay.ToString(),
                            Status = project.Allowed
                        };// Start day dau :(

            ViewBag.LstProject = query.OrderByDescending(x => x.ID).ToList();
            return View();
        }
        public ActionResult viewInfor_Project(int? id)
        {

            var project = db.APMT_Project.SingleOrDefault(x => x.ID == id);
            return View(project);

        }
        [HttpPost]
        public ActionResult Add_New(FormCollection f)
        {
            APMT_Project p = new APMT_Project();
            p.Company_Id = 1;
            p.Manager_Id = 7;
            p.Allowed = true;
            p.Name = f["name"];
            p.Description = f["Descriptions"];
            CultureInfo provider = CultureInfo.InvariantCulture;
            p.StartDay = DateTime.Parse(f["startday"],provider);
            p.EndDay = DateTime.Parse(f["endday"],provider);
            db.APMT_Project.Add(p);
            db.SaveChanges();
            ViewBag.Message = "Successful";
            return RedirectToAction("View_List");
        }
        public ActionResult Delete(int? id)
        {
            var pro = db.APMT_Project.SingleOrDefault(x => x.ID == id);
            db.APMT_Project.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
        public List<APMT_User> getUser()
        {
            var query = from UserC in db.APMT_Company_User
                        join user in db.APMT_User on UserC.User_id equals user.ID
                        join company in db.APMT_Company on UserC.Company_id equals company.ID
                        where company.ID == 1
                        select new APMT_User { Fullname = user.Fullname, Email = user.Email, ID = user.ID };
            return query.OrderByDescending(x => x.ID).ToList();

        }
        public ActionResult autocomplete(string term)
        {
            var query = from user in db.APMT_User
                        where (user.Email.Contains(term) || user.Fullname.Contains(term)) && user.Allowed == 1
                        select user.Email + " (" + user.Fullname + ")";
            var filteredItems = query.ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deleteMember(int? id)
        {
            try
            {
                var user = db.APMT_Company_User.FirstOrDefault(x => x.ID == id);
                db.APMT_Company_User.Remove(user);
                db.SaveChanges();
                return RedirectToAction("View_List");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Can't delete this member!";
                return RedirectToAction("View_List");
            }
        }

        public ActionResult AddMember(FormCollection f)
        {
            //string trimEmail = "";
            //string email = f["somevalue"];
            //// string role = f["selectRole"];
            //if (email.Contains("("))
            //{
            //    trimEmail = email.Substring(0, email.IndexOf('(')).Trim();
            //}
            //else
            //{
            //    trimEmail = email;
            //}
            //try
            //{
            //    var user = db.APMT_User.SingleOrDefault(x => x.Email.Equals(trimEmail));
            //    var userID = user.ID;
            //    if (userID != null)
            //    {
            //        if (user.Allowed == 1)
            //        {
            //            var existMember = db.APMT_Company_User.SingleOrDefault(x => x.User_id == userID);
            //            if (existMember == null)
            //            {
            //                APMT_Company_User companyUser = new APMT_Company_User();
            //                companyUser.Company_id = 1;
            //                companyUser.User_id = int.Parse(userID.ToString());
            //                companyUser.Allowed = 1;
            //                companyUser.isMember = true;
            //                companyUser.isAdministrator = false;
            //                companyUser.isCreator = false;
            //                db.APMT_Company_User.Add(companyUser);
            //                db.SaveChanges();
            //                TempData["Message"] = "Successful";
            //                return RedirectToAction("View_List");
            //            }
            //            else
            //            {
            //                TempData["Message"] = "This user was existed in this company!";
            //                return RedirectToAction("View_List");
            //            }
            //        }
            //        else
            //        {
            //            TempData["Message"] = "This user was Blocked";
            //            return RedirectToAction("View_List");
            //        }
            //    }
            //    return RedirectToAction("View_List");
            //}
            //catch (Exception e)
            //{
            //    TempData["Message"] = "Add new Failure !";
            //    return RedirectToAction("View_List");
            //}
            return View();
        }
        [HttpGet]
        public ActionResult OpenProject(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //APMT_Project a = db.APMT_Project.Find(id);
            //if (a == null)
            //{
            //    return HttpNotFound();
            //}
            //var query = from r_process in db.APMT_Running_Process_Detail
            //            join project in db.APMT_Project on r_process.project_id equals project.id
            //            join user in db.APMT_Users on r_process.leader_id equals user.id
            //            join processa in db.APMT_Process on r_process.process_id equals processa.id
            //            where project.id == id
            //            select new lstProcess_Running { name_project = project.name, email_admin = user.Email, process_targetdate = r_process.targetdate, name_process = processa.name };
            ////  var lstprocess = db.APMT_Running_Process_Detail.Where(x => x.project_id == id).ToList();
            //ViewBag.List = query.ToList();
            return View();

        }
    }
}