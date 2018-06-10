using System.ComponentModel.DataAnnotations;

namespace DataMapping.MetaData
{
    public class OccasionVacationMetaData
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string Date { get; set; }
        
    }
}
