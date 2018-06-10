using System;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.Services
{
   public class StandUpMeetingDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string YesterdayJob { get; set; }
        [Required]
        public string TodayJob { get; set; }
        public string YesterdayObstruction { get; set; }
        public string Reading { get; set; }
        public string Suggestion { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Image { get; set; }
        public int YasterdayJobDegree { get; set; }
        public int TodayJobDegree { get; set; }
        public int ReadingDegree { get; set; }
        public int TotalDegree { get; set; }
        public int SuggestionDegree { get; set; }
        public int StandUpEmployeePointId { get; set; }
        public int SuggestionEmployeePointId { get; set; }

    }
}
