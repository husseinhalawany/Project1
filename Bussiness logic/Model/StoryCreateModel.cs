using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class StoryCreateModel
    {
        public int ProjectId { get; set; }
        public List<CreareOption> options { get; set; }
        public int selectedType { get; set; }
        public StoryCreateModel()
        {
            this.options = new List<CreareOption>();
            this.options.Add(new CreareOption() { id = 1, Type = "Exist Story" });
            this.options.Add(new CreareOption() { id = 2, Type = "New Story" });
        }
    }
    public class CreareOption
    {
        public int id { get; set; }
        public string Type { get; set; }

    }
}
