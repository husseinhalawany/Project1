using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UsersRepository
    {
        public static int GetUserRole(int userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserId == userId);
                int userRoleId = user.webpages_Roles.FirstOrDefault().RoleId;
                return userRoleId;
            }
        }

        public static int GetAdminRole()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.webpages_Roles.FirstOrDefault(x => x.RoleName== "Admin").RoleId;
            }
        }
    }
}
