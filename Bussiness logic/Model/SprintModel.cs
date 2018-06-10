using DataMapping.Entities;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class SprintModel
    {
        public List<Sprint> Previous { get; set; }
        public Sprint Current { get; set; }
        public List<Sprint> Future { get; set; }
    }
}
