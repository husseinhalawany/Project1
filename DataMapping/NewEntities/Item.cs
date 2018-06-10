using DataMapping.Interfaces;
using DataMapping.MetaData;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.Entities
{
    [MetadataType(typeof(ItemMetaData))]
    public partial class Item : IResult
    {
        public bool IsIncludedInSelectedSprint { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
