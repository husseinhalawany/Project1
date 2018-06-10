using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.Services
{
    public class SprintProjectsDetails
    {
        public Project Project { get; set; }
        public int IncludedStoriesCount { get; set; }
    }
}
