﻿using Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace APMT.Areas.System.Controllers
{
    public class ClientManagementController : Controller
    {
        private CP_SPMEntities1 db = new CP_SPMEntities1();

        // GET: System/Client
        public ActionResult View_List(string searchString)
        {
            if (searchString != null)
            {
                var query = from company in db.APMT_Company
                            where (company.Name.Contains(searchString))
                            select company;

                ViewBag.List = query.OrderByDescending(x => x.ID).ToList();

                int count = query.ToList().Count();

                if (count > 0 && searchString.Length != 0)
                {
                    ViewBag.Result = "Finded " + count + " Result(s)";
                }
                else if (count > 0 && searchString.Length == 0)
                {
                    ViewBag.Result = "";
                }
                else
                {
                    ViewBag.Result = "Haven't result to find";
                    var query1 = from company in db.APMT_Company
                                 where (company.Name.Contains(searchString))
                                 select company;

                    ViewBag.List = query1.OrderByDescending(x => x.ID).ToList();
                }
                return View();
            }
            else
            {
                var lstCompany = db.APMT_Company.ToList();

                ViewBag.List = lstCompany;

                ViewBag.Count = lstCompany.Count();

                return View();
            }
        }

        public JsonResult setStatus(int? id)
        {
            bool status = true;
            var company = db.APMT_Company.FirstOrDefault(x => x.ID == id);
            if (company.Allowed == true)
            {
                company.Allowed = false;
                company.Update_at = DateTime.Now;
                status = false;
            }
            else
            {
                company.Allowed = true;

                company.Update_at = DateTime.Now;
                status = true;
            }
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}