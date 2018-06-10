using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.Services
{
    public class EmployeePointDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicURL { get; set; }
        public double TotalPoints { get; set; }
        public int RankNumber { get; set; }
        public string QuarterName { get; set; }
        public string MonthName { get; set; }
        public string TitleName { get; set; }
    }
}
