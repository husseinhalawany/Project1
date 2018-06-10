using DataMapping.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Model
{
    public class StoryItemsModel
    {
        public string StoryName { get; set; }
        public int StoryId { get; set; }
        public List<Item> StoryItemsList { get; set; }
    }
}
