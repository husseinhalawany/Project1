using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;
using DataMapping.Interfaces;

namespace DataMapping.Entities
{
    public partial class Project
    {
        public int NumberOfStories { get; set;}
        public int SprintId { get; set; }
    }
}
