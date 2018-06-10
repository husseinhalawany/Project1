using System.Collections.Generic;
using DataMapping.Entities;
using DataMapping.Interfaces;

namespace BusinessLogic.Model
{
    public class CreateWorkFromHomeRequest : IResult
    {
        public EmployeeVacation EmployeeVacation { get; set; }
        public int RemainingDaysPerYear { get; set; }
        public int RemainingDaysPerMonth { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}