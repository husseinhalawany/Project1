using DataMapping.Interfaces;
using DataMapping.JSONData;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangamentProject.Model
{
    public class UsersIndexModel :IResult
    {
        public List<UserData> UsersList { get; set; }
       public  bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
