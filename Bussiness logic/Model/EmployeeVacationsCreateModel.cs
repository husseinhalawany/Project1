using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;
using DataMapping.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Model
{
    public class CreateEmployeeVacationModel : IResult
    {
        public List<VacationType> VacationTypesList { get; set; }
        public EmployeeVacation EmployeeVacation { get; set; }
        public bool IsAdmin { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
