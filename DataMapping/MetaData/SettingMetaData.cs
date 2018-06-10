using System.ComponentModel.DataAnnotations;

namespace DataMapping.MetaData
{
    public class SettingMetaData
    {
        [Required(ErrorMessage = "Config Key is required")]
        [MaxLength(50)]
        public string ConfigKey { get; set; }
        [Required(ErrorMessage = "Value is required")]
        [MaxLength(200)]
        public string Value { get; set; }
        
    }
}
