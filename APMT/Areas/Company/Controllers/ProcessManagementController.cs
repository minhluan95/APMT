using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace APMT.Areas.Company.Controllers
{
    public class ProcessManagementController : Controller
    {
        //GET: Company/ManageProcess
        public ActionResult ListProcess()
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
            //  var lstprocess = db.APMT_Running_Process_Detail.Where(x => x.project_id == id).ToList();
            //ViewBag.List = query.ToList();
            return View();

        }
        public ActionResult AddMember()
        {
            return View();
        }
    }
}