//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMapping.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeePoint
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public Nullable<int> ActionRateId { get; set; }
        public Nullable<int> UserId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ActionRate ActionRate { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
