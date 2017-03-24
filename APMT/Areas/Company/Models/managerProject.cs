using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APMT.Areas.Company.Models
{
    public class managerProject
    {
        public int ID { set; get; }

        public string ProjectName { set; get; }

        public string ManagerName { set; get; }

        public string StartDay { set; get; }

        public string EndDay { set; get; }

        public bool? Status { set; get; }

        public string Description { set; get; }
    }
}