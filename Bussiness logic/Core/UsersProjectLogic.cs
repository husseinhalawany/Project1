using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;
using BusinessLogic.Helpers;
using DataAccess.Repositories;
using DataMapping.Services;
using DataMapping.Entities;

namespace BusinessLogic.Core
{
    public class UsersProjectLogic
    {
        public static List<UsersProjectModel> GetUserProjectsList(int projectId)
        {
            return UsersProjectRepositories.GetUserProjectsList(projectId);
        }
        public static UsersProjectUpdateDetails GetUserProjectUpdateModel(int userId, int projectId)
        {
            UsersProjectUpdateDetails model = new UsersProjectUpdateDetails()            
            {
                userProject = GetUserProject(userId, projectId),
                projectRoles = ProjectRolesRepositories.GetProjectRolesList(),

            };
            model.To = model.userProject.UserName;
            return model;
        }
        public static UsersProjectUpdateDetails GetUserProjectModelForCreate(int projectId)
        {
            return new UsersProjectUpdateDetails()
            {
                userProject = new UsersProjectModel() { ProjectId = projectId },
                projectRoles = ProjectRolesRepositories.GetProjectRolesList(),
                users = GetUsersNotInProject(projectId)
            };
        }

        private static List<UserProfile> GetUsersNotInProject(int projectId)
        {
            return UsersProjectRepositories.GetUsersNotInProject(projectId);
        }

        public static void DeleteUserFromProject(int userId, int projectId)
        {
            UsersProjectRepositories.DeleteUserFromProject(userId, projectId);
        }

        public static UsersProjectModel GetUserProject(int userId, int projectId)
        {
            return UsersProjectRepositories.GetUserProject(userId, projectId);

        }

        public static void UpdateUserProject(UsersProjectModel userProject)
        {
            DateTime myDate = DateTimeHelper.Today();
            UsersProjectRepositories.UpdateUserProject(userProject, myDate);

        }

        public static void CreateUserProject(UsersProjectModel userProject)
        {
            DateTime myDate = DateTimeHelper.Today();
            DataMapping.Entities.UserProfile user = UserProfilesLogic.GetUserByUserName(userProject.UserName);
            userProject.UserId = user.UserId;
            UsersProjectRepositories.CreateUserProject(userProject, myDate);

        }
    }
}
