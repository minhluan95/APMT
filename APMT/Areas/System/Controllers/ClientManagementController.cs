using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace APMT.Areas.System.Controllers
{
    public class ClientManagementController : Controller
    {
        CP_SPMEntities1 db = new CP_SPMEntities1();
        // GET: System/Client
        public ActionResult View_List()
        {
            var lstCompany = db.APMT_Company.ToList();
            return View(lstCompany);
        }
    }
}