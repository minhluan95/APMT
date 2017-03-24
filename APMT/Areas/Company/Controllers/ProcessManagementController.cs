using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Models;
namespace APMT.Areas.Company.Controllers
{
    public class ProcessManagementController : Controller
    {
        //GET: Company/ManageProcess
        CP_SPMEntities1 db = new CP_SPMEntities1();
        public ActionResult View_List()
        {
            var lstProcess = db.APMT_Process.ToList().OrderByDescending(x => x.ID).ToList();
            ViewBag.lstProcess = lstProcess;
            return View();

        }
        public ActionResult AddMember()
        {
            return View();
        }
    }
}