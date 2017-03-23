using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Data.Entity;

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
        public ActionResult setStatus(int? id)
        {
            var company = db.APMT_Company.FirstOrDefault(x => x.ID == id);
            if (company.Allowed == true)
            {
                company.Allowed = false;
                company.Update_at = DateTime.Now;
            }
            else
            {
                company.Allowed = true;
                company.Update_at = DateTime.Now;
            }
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("View_List");
        }
    }
}