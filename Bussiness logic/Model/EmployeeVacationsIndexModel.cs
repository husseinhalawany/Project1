using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;

namespace BusinessLogic.Model
{
    public class EmployeeVacationsIndexModel
    {
        public List<VacationStatu> VacationStatusList { get; set; }
        public List<UserProfile> EmployeeUsersList { get; set; }
        public int StatusId { get; set; }
        public int EmployeeUserId { get; set; }
        
       
    }
}
