using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;
using DataMapping.Interfaces;

namespace DataMapping.Entities
{
    [MetadataType(typeof(VacationTypeMetaData))]
    public partial class VacationType : IResult
    {
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}
