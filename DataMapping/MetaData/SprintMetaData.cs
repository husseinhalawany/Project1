using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.MetaData
{
    public class SprintMetaData
    {
        [Required]
        public string Name { get; set; }
    }
}
