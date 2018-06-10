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
    
    public partial class StandUpMeeting
    {
        public int Id { get; set; }
        public string YesterdayJob { get; set; }
        public string TodayJob { get; set; }
        public string YesterdayObstruction { get; set; }
        public string Reading { get; set; }
        public string Suggestion { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int YesterdayJobDegree { get; set; }
        public int TodayJobDegree { get; set; }
        public int ReadingDegree { get; set; }
        public int SuggestionDegree { get; set; }
        public int TotalDegree { get; set; }
        public int StandUpEmployeePointId { get; set; }
        public int SuggestionEmployeePointId { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}