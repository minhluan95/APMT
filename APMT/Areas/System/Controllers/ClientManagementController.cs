using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APMT.Areas.System.Controllers
{
    public class ClientManagementController : Controller
    {
        // GET: System/Client
        public ActionResult View_List()
        {
            return View();
        }
    }
}