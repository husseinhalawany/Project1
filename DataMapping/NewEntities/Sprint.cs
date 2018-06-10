using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;


namespace DataMapping.Entities
{
    [MetadataType(typeof(SprintMetaData))]
    public partial class Sprint
    {
        public bool IsSelected { get; set; } 
    }
}
