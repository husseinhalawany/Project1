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
    
    public partial class WorkFromHomeDay
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeUserId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> VacationYearId { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual VacationStatu VacationStatu { get; set; }
        public virtual VacationYear VacationYear { get; set; }
    }
}