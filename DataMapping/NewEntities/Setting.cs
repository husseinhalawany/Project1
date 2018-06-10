using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;
using DataMapping.Interfaces;
namespace DataMapping.Entities
{
    [MetadataType(typeof(SettingMetaData))]
    public partial class Setting : IResult
    {
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}