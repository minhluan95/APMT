using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace APMT.Areas.Company.Models
{
    public class projectMember
    {
        public int ID { get; set; }
        public int ID_project { get; set; }
        public int ID_User { get; set; }
        public String fullName { get; set; }
        public String email { get; set; }
        public int? Allowed { get; set; }
   

    }
}