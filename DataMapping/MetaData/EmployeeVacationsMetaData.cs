using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataMapping.MetaData
{
    public class EmployeeVacationsMetaData
    {

        public int Id { get; set; }
        public int StatusId { get; set; }

        [Required(ErrorMessage = "You must choose user")]
        public int EmployeeUserId { get; set; }
        [Required(ErrorMessage = "You must chooser vacation type")]
        public int VacationTypeId { get; set; }
        public int VacationDays { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
