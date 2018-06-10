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
    [MetadataType(typeof(EmployeeVacationsMetaData))]
    public partial class EmployeeVacation : IResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
