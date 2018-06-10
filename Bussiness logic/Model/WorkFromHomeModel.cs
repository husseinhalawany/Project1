using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class WorkFromHomeModel 
    {
        public WorkFromHomeDay WorkFromHomeDay { get; set; }
        public int RemainingDaysPerYear { get; set; }
        public int RemainingDaysPerMonth { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
