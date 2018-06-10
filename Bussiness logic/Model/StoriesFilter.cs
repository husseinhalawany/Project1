using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Services;
using DataMapping.Entities;
namespace BusinessLogic.Model
{
    public class StoriesFilter
    {
        public List<StoriesDetails> AllStories { set; get; }
        public SprintModel Sprints { get; set; }
        public List<Item> SprintItems { get; set; }
        public List<Sprint> Iterations { get; set; }
        public string SearchText { get; set; }
        public int sprintId { get; set; }
        public bool Reviewed { get; set; }
        public bool OrderedByName { get; set; }
        public int projectId { get; set; }
    }
}