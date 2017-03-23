using System.Web.Mvc;

namespace APMT.Areas.Company
{
    public class CompanyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Company";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Company_default",
                "{areas}/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}