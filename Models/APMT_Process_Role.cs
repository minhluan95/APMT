
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
    
public partial class APMT_Process_Role
{

    public int ID { get; set; }

    public string Name { get; set; }

    public Nullable<int> Process_Id { get; set; }



    public virtual APMT_Process APMT_Process { get; set; }

}

}
