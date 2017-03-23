using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APMT.Areas.Company.Controllers
{
    public class DefineProcessController : Controller
    {
        [Route("/company/define-process")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("/company/process-info")]
        public ActionResult Define()
        {
            return View();
        }
        [Route("/company/activity-config")]
        public ActionResult StepConfig()
        {
            return View();
        }
        [Route("/company/setup-task")]
        public ActionResult SetupActivity()
        {
            return View();
        }
        public ActionResult ManageRole()
        {
            return View();
        }
        [Route("/company/document-in-process")]
        public ActionResult ManageDocument()
        {
            return View();
        }
    }
}