using DataMapping.Entities;
using System.Collections.Generic;

namespace DataMapping.Services
{
    public class StoriesDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int ContainItem { get; set; }
        public List<Sprint> Sprints { get; set; }
    }
}