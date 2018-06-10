using DataMapping.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class RolesRepositories
    {
        public static webpages_Roles GetRoleById(int roleId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.webpages_Roles.FirstOrDefault(r => r.RoleId == roleId);
            }
        }
        public static webpages_Roles GetEmployeeRole()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.webpages_Roles.FirstOrDefault(r => r.RoleName == "Employee");

            }
        }

        public static void CreateUser(int UserId, int RoleId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(a => a.UserId == UserId);
                if (user != null)
                {

                    webpages_Roles userRole = db.webpages_Roles.FirstOrDefault(a => a.RoleId == RoleId);
                    user.webpages_Roles.Add(userRole);
                    db.SaveChanges();
                }
            }
        }
    }
}