using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.MetaData
{
    public class SuggestionMeataData
    {
        [Required(ErrorMessage="Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        

    }
}
