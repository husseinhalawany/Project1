using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussinesslogic.Model
{
    public class HomeIndexModel
    {
        public List<Project> Projects { get; set; }
        public bool CanCreateSprint { get; set; }
        public Sprint PreviousSprint { get; set; }
        public Sprint CurrentSprint { get; set; }
        public Sprint FutureSprint { get; set; }
    }
}
