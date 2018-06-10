using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.MetaData
{
    public class VacationTypeMetaData
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "VacationLength is Required")]
        public string VacationLength { get; set; }
    }
}
