using DataMapping.Interfaces;
using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.Entities
{
    [MetadataType(typeof(OccasionVacationMetaData))]
    public partial class OccasionVacation : IResult
    {
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? VacationYearStatusId { get; set; }
    }
}
