using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace APMT.Areas.Company.Models
{
    public class AuthorizeSystem : ActionFilterAttribute
    {
        private CP_SPMEntities1 db = new CP_SPMEntities1();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["userid"] == null)
            {
                filterContext.Result = new RedirectResult("/company/log-in");
                return;
            }
            int userid = int.Parse(HttpContext.Current.Session["userid"].ToString());

            string actionname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller_" + filterContext.ActionDescriptor.ActionName;

            //var admin = db.APMT_ProductAdmin.Where(a => a.UserId == userid && a.isAdmin.Value != 0).FirstOrDefault();
            //if (admin != null)
            //{
            //    return;
            //}
            var listpermission = from a in db.APMT_Action
                                 join p in db.APMT_Permission on a.ID equals p.ActionId
                                 where p.UserId == userid
                                 select a.ActionName;
            if (!listpermission.Contains(actionname))
            {
                filterContext.Result = new RedirectResult("/page-not-found");
                return;
            }
        }
    }
}