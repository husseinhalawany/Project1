using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;

namespace DataMapping.Services
{
    public class EmplyeeVacationDetails
    {
        public int Id { get; set; }
        public int? EmployeeUserId { get; set; }
        public string EmployeeUserName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? VacationTypeId { get; set; }
        public string VacationTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int VacationDays { get; set; }
        
    }
}
