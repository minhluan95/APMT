using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APMT.Areas.Company.Controllers
{
    public class GeneralSettingController : Controller
    {
        // GET: Company/ManageCompanyInfo
        #region Register Company
        public ActionResult CheckEmail1()
        {
            return View();
        }
        public ActionResult YourName2()
        {
            return View();
        }
        public ActionResult SetYourPassword3()
        {
            return View();
        }
        public ActionResult AboutYourTeam4()
        {
            return View();
        }
        public ActionResult CompanyCalled5()
        {
            return View();
        }
        public ActionResult WebDomain6()
        {
            return View();
        }
        public ActionResult Invitation7()
        {
            return View();
        }
        #endregion

        public ActionResult Update_Company_Information()
        {
            return View();
        }
        public ActionResult Change_Language()
        {
            return View();
        }
    }
}