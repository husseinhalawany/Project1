using System;

namespace DataMapping.Services
{
    public class RequestFromHomeDayDetails
    {
        public int Id { get; set; }
        public int? EmployeeUserId { get; set; }
        public string EmployeeUserName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime Date { get; set; }
        public int? VacationYearId { get; set; }
        public string VacationYearName { get; set; }
    }
}
