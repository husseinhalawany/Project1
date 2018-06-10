using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class SuggetionIndexModel
    {
        public List<Project> ProjectsList { get; set; }
        public int projectId { get; set; }
    }
}
