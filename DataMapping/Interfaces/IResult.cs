using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.Interfaces
{
    public interface IResult
    {
        bool Succeeded { get; set; }
        string ErrorMessage { get; set; }

    }
}
