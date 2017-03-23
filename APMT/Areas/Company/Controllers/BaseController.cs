
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace APMT.Areas.Company.Controllers
{
    public class BaseController : Controller
    {
        
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CurentCulture"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CurentCulture"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurentCulture"].ToString());
            }
            else
            {
                Session["CurentCulture"] = "en";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }

        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session["CurentCulture"] = ddlCulture;
            return Redirect(returnUrl);
        }
    }
}