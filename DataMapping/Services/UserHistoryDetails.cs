using System;

namespace DataMapping.Services
{
    public class UserHistoryDetails
    {
        public int EmployeeId { set; get; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double CurrentSalary { get; set; }
        public double PreviousSalary { get; set; }
        public int LastIncrementPercentage { get; set; }
        public DateTime JoinDate { get; set; }
        public int Days { set; get; }
        public int Weeks { set; get; }
        public int Months { set; get; }
        public int Years { set; get; }
    }
}