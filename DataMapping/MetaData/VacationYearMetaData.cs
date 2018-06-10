using System.ComponentModel.DataAnnotations;

namespace DataMapping.MetaData
{
    public class VacationYearMetaData
    {
        [Required(ErrorMessage = "Year Name is Required")]
        public string YearName { get; set; }

        [Required(ErrorMessage = "Start Date is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "End Date is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string EndDate { get; set; }
    }
}
