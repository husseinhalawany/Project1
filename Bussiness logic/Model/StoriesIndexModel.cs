using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class StoriesIndexModel
    {
        public string projectName { get; set; }
        public int projectId { get; set; }
        public bool Reviewed { get; set; }
        public bool OrderedByName { get; set; }

        public string SearchText { get; set; }
    }
}
