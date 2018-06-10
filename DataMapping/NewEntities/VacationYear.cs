using DataMapping.Interfaces;
using DataMapping.MetaData;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.Entities
{
    [MetadataType(typeof(VacationYearMetaData))]
    public partial class VacationYear : IResult
    {
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
        public int VacationYearStatusId { get; set; }
        public int TitleName { get; set; }
    }
}
