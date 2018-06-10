using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class OnlineUserModel
    {

        public List<SignedEmployee> OnlineUsers { get; set; }
        public List<SignedEmployee> LeaveUser { get; set; }
        public List<SignedEmployee> NotComeUser { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
