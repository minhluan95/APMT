
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
    
public partial class APMT_Activity_to_Activity
{

    public int ID { get; set; }

    public Nullable<int> FromAc { get; set; }

    public Nullable<int> ToAC { get; set; }

    public Nullable<int> Process_ID { get; set; }



    public virtual APMT_Activity APMT_Activity { get; set; }

    public virtual APMT_Activity APMT_Activity1 { get; set; }

    public virtual APMT_Process APMT_Process { get; set; }

}

}