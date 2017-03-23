using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace APMT.Areas.Company.Controllers
{
    public class ProjectManagementController : Controller
    {
        // GET: Company/ManageProject
        //APMTDbContext db = new APMTDbContext();
        public ActionResult View_List()
        {
           // ViewBag.LstProject = db.APMT_Project.OrderByDescending(x => x.id).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add_New(FormCollection f)
        {
            //APMT_Project p = new APMT_Project();
            //p.company_id = 1;
            //p.manager_id = 1;
            //p.status = "1";
            //p.name = f["name"];
            //p.Descriptions = f["Descriptions"];
            //db.APMT_Project.Add(p);
            //db.SaveChanges();
            //ViewBag.Message = "Successful";
            return RedirectToAction("Index");
        }
        public ActionResult AddMember()
        {
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