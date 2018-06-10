using System.ComponentModel.DataAnnotations;

namespace DataMapping.MetaData
{
    public class ItemMetaData
    {
        [Required]
        public string Name { get; set; }
    }
}
