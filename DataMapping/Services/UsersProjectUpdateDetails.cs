using System.Collections.Generic;
using DataMapping.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataMapping.Services
{
    public class UsersProjectUpdateDetails
    {
        public UsersProjectModel userProject { get; set; }
        public List<ProjectRole> projectRoles { get; set; }
        [Required]
        public string To { get; set; }
        public List<UserProfile> users { get; set; }
        public List<UsersProjectModel> usersInProject { get; set; }

    }
}
