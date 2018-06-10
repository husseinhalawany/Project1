using DataMapping.Entities;
using System.Collections.Generic;

namespace DataMapping.Services
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public List<UsersProjectModel> UsersList { get; set; }
        public Sprint Sprint { get; set; }
    }
}
