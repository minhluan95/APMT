using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Models;
using APMT.Areas.Company.Models;
using System.Globalization;
using System.Data.Entity;

namespace APMT.Areas.Company.Controllers
{
    public class ProjectManagementController : Controller
    {
        int CompanyID = 1;
        CP_SPMEntities1 db = new CP_SPMEntities1();
        public ActionResult View_List()
        {
           var query = from project in db.APMT_Project
                        join user_company in db.APMT_Company_User on project.Manager_Id equals user_company.ID
                        join user in db.APMT_User on user_company.User_id equals user.ID
                        where project.Company_Id == CompanyID && project.Allowed == true
                        select new managerProject
                        {
                            ID = project.ID,
                            ProjectName = project.Name,
                            ManagerName = user.Fullname,
                            StartDay = project.StartDay.ToString(),
                            EndDay = project.EndDay.ToString(),
                            Status = project.Allowed
                        };
            List<managerProject> lstProject = query.OrderByDescending(x => x.ID).ToList();
            ViewBag.LstProject = lstProject;
            return View();
        }
        public ActionResult viewInfor_Project(int? id)
        {

            var project = db.APMT_Project.SingleOrDefault(x => x.ID == id);
            return View(project);

        }
        [HttpPost]
        public ActionResult Add_New_Project(FormCollection f)
        {
            string trimEmail = "";
            string email = f["txt_PMember_Email"];
            if (email.Contains("("))
            {
                trimEmail = email.Substring(0, email.IndexOf('(')).Trim();
            }
            else
            {
                trimEmail = email;
            }
            var user = db.APMT_User.SingleOrDefault(x => x.Email.Equals(trimEmail));
            if (user != null)
            {
                var userID = user.ID;
                if (user.Allowed == 1)
                {
                    var existMember = db.APMT_Company_User.SingleOrDefault(x => x.User_id == userID);
                    if (existMember != null)
                    {
                        APMT_Project p = new APMT_Project();
                        p.Company_Id = existMember.ID;
                        p.Manager_Id = userID;
                        p.Allowed = true;
                        p.Name = f["name"];
                        p.Description = f["Descriptions"];
                        CultureInfo provider = CultureInfo.InvariantCulture;
                        p.StartDay = DateTime.Parse(f["startday"], provider);
                        p.EndDay = DateTime.Parse(f["endday"], provider);
                        db.APMT_Project.Add(p);
                        db.SaveChanges();
                        ViewBag.Message = "Successful";
                        return RedirectToAction("View_List");
                    }
                    else
                    {
                        //mesg = "This user was existed in this company!";
                        TempData["Message"] = "This user is not existed in this company!";
                        return RedirectToAction("View_List");

                    }
                }
                else
                {
                    //mesg = "This user was Blocked";
                    TempData["Message"] = "This user was Blocked";
                    return RedirectToAction("View_List");

                }
            }
            else
            {
                //mesg = "This user not Exist";
                TempData["Message"] = "This user not Exist";
                return RedirectToAction("View_List");

            }
        }
        public ActionResult autocompleteP_Member(string term)
        {
            var query = from compUser in db.APMT_Company_User
                        join user in db.APMT_User on compUser.User_id equals user.ID
                        where (user.Email.Contains(term) || user.Fullname.Contains(term)) && compUser.Allowed == 1 && compUser.Company_id == CompanyID
                        select user.Email + " (" + user.Fullname + ")";
            var filteredItems = query.ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult View_Members(int id)
        {
                var query = from P_member in db.APMT_Project_User
                            join compUser in db.APMT_Company_User on P_member.User_company_id equals compUser.ID
                            join user in db.APMT_User on compUser.User_id equals user.ID
                            join project in db.APMT_Project on P_member.Project_id equals project.ID
                            where P_member.Project_id == id
                            select new projectMember
                            {
                                ID = P_member.ID,
                                ID_project = id,
                                ID_User = user.ID,
                                fullName = user.Fullname,
                                Allowed = P_member.Allowed,
                                email = user.Email,
                                projectName = project.Name
                            };
                ViewBag.LstMember = query.OrderByDescending(x => x.ID).ToList();
            return View();

        }
        [HttpPost]
        public ActionResult AddMember(FormCollection f,APMT_Project project)
        {
            string trimEmail = "";
            if (f != null)
            {
                string email = f["txt_PMember_Email"];
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
                                APMT_Project_User projectUser = new APMT_Project_User();
                                projectUser.User_company_id = userID;
                                projectUser.Project_id = project.ID;
                                projectUser.Allowed = 1;
                                db.APMT_Project_User.Add(projectUser);
                                db.SaveChanges();
                                //mesg = "Successful";
                                TempData["Message"] = "Successful";
                                return View();

                            }
                            else
                            {
                                //mesg = "This user was existed in this company!";
                                TempData["Message"] = "This user was existed in this company!";
                                return View();

                            }
                        }
                        else
                        {
                            //mesg = "This user was Blocked";
                            TempData["Message"] = "This user was Blocked";
                            return View();

                        }
                    }
                    else
                    {
                        //mesg = "This user not Exist";
                        TempData["Message"] = "This user not Exist";
                        return View();

                    }


                }
                catch (Exception e)
                {
                    //mesg = "Add new Failure,Please Try Again !";
                    TempData["Message"] = "Add new Failure !";
                    return View();

                }

            }
            else
            {
                TempData["Message"] = "Please Input Email";
                return View();

            }
            //return Json(new
            //{
            //    mesg
            //}, JsonRequestBehavior.AllowGet);

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
        
        public JsonResult setStatus(int? id)
        {
            bool status = true;
            var user = db.APMT_Project_User.FirstOrDefault(x => x.ID == id);
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = db.APMT_Project_User.FirstOrDefault(x => x.ID == id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var user = db.APMT_Project_User.FirstOrDefault(x => x.ID == id);
            db.APMT_Project_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("AddMember");
        }
        [HttpPost]
        public ActionResult UpdateActivity([Bind(Include = "ID,Name,Description,Weigth,Process_id,Type_id,activity_key,Category")]APMT_Activity a)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SetupActivity", new { id = a.Process_Id });
            }
            return View(a);
        }
    }
}