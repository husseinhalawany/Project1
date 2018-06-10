using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangamentProject.Core
{
    public class UsersLogic
    {

        public static int GetUserRole(int userId)
        {
            return UsersRepository.GetUserRole(userId);
        }
        public static int GetAdminRole()
        {
            return UsersRepository.GetAdminRole();
        }
       
    }
}
