using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using DataMapping.Services;
using DataMapping.Enums;

namespace DataAccess.Repositories
{
    public class UsersProjectRepositories
    {
        public static List<UsersProjectModel> GetUserProjectsList(int projectId)
        {

            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from proj in db.Projects.Where(x => x.Id == projectId)
                        join projectRoles in db.ProjectUsers
                            on proj.Id equals projectRoles.ProjectId
                        join roles in db.ProjectRoles
                            on projectRoles.RoleId equals roles.Id
                        join user in db.UserProfiles.Where(r => r.LockedUser == false)
                            on projectRoles.UserId equals user.UserId

                        select new UsersProjectModel()
                        {
                            UserId = user.UserId,
                            UserName = GetUserNameFormate(user.FirstName, user.LastName),
                            ProjectId = proj.Id,
                            ProjectName = proj.Name,
                            roleId = roles.Id,
                            RoleName = roles.RoleName,
                            NickName = GetUserNameFormate(user.FirstName, user.LastName),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            CreatorId = proj.CreatorId.GetValueOrDefault()
                        })
                        .ToList();
                return q;
            }
        }
        public static string GetUserNameFormate(string first, string last)
        {
            string username = "";
            for (int i = 0; i < first.Length; i++)
            {
                if (char.IsLetter(first[i]))
                {
                    username += first[i].ToString().ToUpper();
                    break;
                }
            }
            for (int i = 0; i < last.Length; i++)
            {
                if (char.IsLetter(last[i]))
                {
                    username += last[i].ToString().ToUpper();
                    break;
                }
            }
            return username;
        }     
        public static UsersProjectModel GetUserProject(int userId, int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from proj in db.Projects
                        join projectRoles in db.ProjectUsers.Where(x => x.UserId == userId && x.ProjectId == projectId)
                        on proj.Id equals projectRoles.ProjectId
                        join roles in db.ProjectRoles
                        on projectRoles.RoleId equals roles.Id
                        join user in db.UserProfiles.Where(r => r.LockedUser == false)
                        on projectRoles.UserId equals user.UserId
                        select new UsersProjectModel()
                        {
                            UserId = user.UserId,
                            UserName = user.UserName,
                            ProjectId = proj.Id,
                            ProjectName = proj.Name,
                            roleId = roles.Id,
                            RoleName = roles.RoleName
                        })
                        .FirstOrDefault();
                return q;

            }
        }

        public static List<UserProfile> GetUsersNotInProject(int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from user in db.UserProfiles
                       
                        join projectRoles in db.ProjectUsers
                            on user.UserId equals projectRoles.UserId
                        where user.UserId == projectRoles.UserId && 
                            projectRoles.ProjectId == projectId
                        && user.webpages_Roles.Any(r => r.RoleName != "Admin")
                        select user;
                List<UserProfile> users_inProject = q.ToList();
                List<UserProfile> model = UserProfilesRepository.GetNotAdminUsers();
                foreach (var item in users_inProject)
                {
                    model.RemoveAll(x => x.UserId == item.UserId);
                }
                return model;
            }
        }
        public static void CreateUserProject(UsersProjectModel userProject, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                ProjectUser model = new ProjectUser()
                {
                    CreateDate = today,
                    LastAssignDate = today,
                    CreatorId = userProject.CreatorId,
                    ProjectId = userProject.ProjectId,
                    RoleId = userProject.roleId,
                    UserId = userProject.UserId
                };
                db.ProjectUsers.Add(model);
                db.SaveChanges();
            }
        }
        public static void UpdateUserProject(UsersProjectModel userProject, DateTime today)
        {
            DeleteUserFromProject(userProject.UserId, userProject.ProjectId);
            CreateUserProject(userProject, today);
        }

        public static void DeleteUserFromProject(int userId, int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var user_in_role = db.ProjectUsers
                    .FirstOrDefault(x => x.UserId == userId && x.ProjectId == projectId);
                db.ProjectUsers.Remove(user_in_role);
                db.SaveChanges();
            }
        }
    }
}
