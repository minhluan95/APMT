
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Models
{

using System;
    using System.Collections.Generic;
    
public partial class APMT_Project
{

    public int ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Nullable<int> Manager_Id { get; set; }

    public Nullable<int> Company_Id { get; set; }

    public Nullable<bool> Allowed { get; set; }



    public virtual APMT_Company APMT_Company { get; set; }

    public virtual APMT_Company_User APMT_Company_User { get; set; }

}

}
