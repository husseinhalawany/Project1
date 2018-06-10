using DataMapping.Interfaces;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangamentProject.Model
{
    public class UserSignsIndexModel : IResult
    {
        public List<UserSign> UserSignsList { get; set; }
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}
