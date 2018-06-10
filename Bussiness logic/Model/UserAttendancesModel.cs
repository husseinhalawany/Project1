using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class UserAttendancesModel
    {
        public List<Attendance> UserAttendances { get; set; }
        public int TotalWorkingDaysInMonth { get; set; }
        public int TotalUserWorkingDays { get; set; }
        public int TotalUserAbsenceDays { get; set; }
        public double TotalUserWorkingHours { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
