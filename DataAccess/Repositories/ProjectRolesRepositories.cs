using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;

namespace DataAccess.Repositories
{
    public class ProjectRolesRepositories
    {
        public static List<ProjectRole> GetProjectRolesList()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.ProjectRoles.ToList();
            }
        }
    }
}
