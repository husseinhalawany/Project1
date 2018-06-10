using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataMapping.MetaData;

namespace DataMapping.Entities
{
    [MetadataType(typeof(SuggestionMeataData))]

    public partial class Story
    {
        public bool Exist { get; set; }

        public int sprintId { get; set; }
        
    }
}
